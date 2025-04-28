using Cobrowse.IO;

namespace MauiSample;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        Title = "Home";
    }

    private void OnCobrowseClicked(object sender, EventArgs e)
    {
        this.Navigation.PushAsync(new CobrowseCodePage());
    }

    private void OnAccessibilitySetupClicked(object sender, EventArgs e)
    {
#if __ANDROID__
        global::Cobrowse.IO.Android.CobrowseAccessibilityService.ShowSetup(
            global::Android.App.Application.Context);
#endif
    }
}


