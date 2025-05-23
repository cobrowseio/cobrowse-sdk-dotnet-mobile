using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cobrowse.IO;

namespace MauiSample;

public partial class CobrowseCodePage : ContentPage
{
    private ISession _session;
    private bool _loadingSession = false;

    private Random _random;
    private bool _animationTimerActive;

    public CobrowseCodePage()
    {
        InitializeComponent();

        _random = new Random();

        viewCodeDiplayWorking.IsVisible = false;

        if (!_animationTimerActive)
        {
            _animationTimerActive = true;
            Device.StartTimer(TimeSpan.FromSeconds(0.08d), () =>
            {
                if (_animationTimerActive)
                {
                    RenderRandomCode();
                }
                return _animationTimerActive;
            });
        }

        InitSession();
        Render();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        CobrowseIO.Instance.SessionDidUpdate += CobrowseAdapter_SessionDidUpdate;
        CobrowseIO.Instance.SessionDidEnd += CobrowseAdapter_SessionDidEnd;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        CobrowseIO.Instance.SessionDidUpdate -= CobrowseAdapter_SessionDidUpdate;
        CobrowseIO.Instance.SessionDidEnd -= CobrowseAdapter_SessionDidEnd;

        _animationTimerActive = false;
    }

    private void InitSession()
    {
        // if we have specific session that we're loading
        // then dont try to bootstrap a session object
        // from anywhere else, just wait for it to finish
        // loading.
        if (!_loadingSession)
        {
            // if the current session looks like it's still active
            // then we'll use that one
            if (CobrowseIO.Instance.CurrentSession?.IsActive == true)
            {
                _session = CobrowseIO.Instance.CurrentSession;
                //[session registerSessionObserver:self];
            }
            else
            {
                // otherwise create a new session
                CreateSession();
            }
        }
    }

    private void CreateSession()
    {
        CobrowseIO.Instance.CreateSession((Exception err, ISession session) =>
        {
            if (err != null)
            {
                RenderError(err);
            }
            else
            {
                _session = session;
                Render();
            }
        });
    }

    private void ShowSubview(View view)
    {
        foreach (var next in viewContainer.Children.OfType<View>())
        {
            next.IsVisible = next == view;
        }
    }

    private void Render()
    {
        if (_session?.IsActive == true)
        {
            ShowSubview(viewManageSession);
        }
        else
        {
            Debug.WriteLine("New session code is " + _session?.Code);
            ShowSubview(viewCodeDisplay);
            SetCodeDisplay(_session?.Code);
        }
    }

    private void RenderError(Exception e)
    {
        Device.BeginInvokeOnMainThread(() =>
        {
            ShowSubview(viewError);
            Debug.WriteLine("CobrowseIO Error: {0}", e);
        });
    }

    private void EndSession(object sender, EventArgs args)
    {
        this.Navigation.PopAsync();
        _session.End((Exception e, ISession session) =>
        {
            if (e != null)
            {
                RenderError(e);
            }
        });
    }

    private void CobrowseAdapter_SessionDidUpdate(object sender, ISession e)
    {
        Render();
    }

    private void CobrowseAdapter_SessionDidEnd(object sender, ISession e)
    {
        InitSession();
        Render();
    }

    public void SetCodeDisplay(string code)
    {
        if (code == null)
        {
            return;
        }

        _animationTimerActive = false;
        this.viewCodeDiplayWorking.IsVisible = true;
        this.viewCodeLabel.Opacity = 1f;
        RenderCode(code);
    }

    private void RenderRandomCode()
    {
        string chars = "1234567890";
        string code = "";
        for (int i = 0; i < 6; i++)
        {
            code += chars[_random.Next() % chars.Length];
        }
        RenderCode(code, 0.1f);
    }

    private void RenderCode(string code, double desiredOpacity = 1d)
    {
        string first3 = code.Substring(0, 3);
        string last3 = code.Substring(3);
        this.viewCodeLabel.Opacity = desiredOpacity;
        this.viewCodeLabel.Text = $"{first3}-{last3}";
    }
}

