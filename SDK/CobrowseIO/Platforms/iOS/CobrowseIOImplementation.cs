using System;
using System.Collections.Generic;
using System.ComponentModel;
using Foundation;
using UIKit;
using Cobrowse.IO.iOS;
using NativeCobrowseIO = Cobrowse.IO.iOS.CobrowseIO;

namespace Cobrowse.IO
{
    /// <summary>
    /// iOS-specific implementation of the cross-platform Cobrowse.io wrapper.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class CobrowseIOImplementation : ICobrowseIO
    {
        /// <inheritdoc/>
        public event EventHandler<ISession>? SessionDidRequest;

        internal bool RaiseSessionDidRequest(Session session)
        {
            EventHandler<ISession>? sessionDidRequest = SessionDidRequest;
            if (sessionDidRequest != null
                && CobrowseSessionImplementation.TryCreate(session) is ISession s)
            {
                sessionDidRequest(this, s);
                return true;
            }
            return false;
        }

        /// <inheritdoc/>
        public event EventHandler<ISession>? RemoteControlRequest;

        internal bool RaiseRemoteControlRequest(Session session)
        {
            EventHandler<ISession>? remoteControlRequest = RemoteControlRequest;
            if (remoteControlRequest != null
                && CobrowseSessionImplementation.TryCreate(session) is ISession s)
            {
                remoteControlRequest(this, s);
                return true;
            }
            return false;
        }

        /// <inheritdoc/>
        public event EventHandler<ISession>? MetricsDidUpdate;

        internal bool RaiseMetricsDidUpdate(Session session)
        {
            EventHandler<ISession>? metricsDidUpdate = MetricsDidUpdate;
            if (metricsDidUpdate != null
                && CobrowseSessionImplementation.TryCreate(session) is ISession s)
            {
                metricsDidUpdate(this, s);
                return true;
            }
            return false;
        }

        /// <inheritdoc/>
        public event EventHandler<ISession>? SessionDidLoad;

        internal bool RaiseSessionDidLoad(Session session)
        {
            EventHandler<ISession>? sessionDidLoad = SessionDidLoad;
            if (sessionDidLoad != null
                && CobrowseSessionImplementation.TryCreate(session) is ISession s)
            {
                sessionDidLoad(this, s);
                return true;
            }
            return false;
        }

        /// <inheritdoc/>
        public event EventHandler<ISession>? SessionDidUpdate;

        internal bool RaiseSessionDidUpdate(Session session)
        {
            EventHandler<ISession>? sessionDidUpdate = SessionDidUpdate;
            if (sessionDidUpdate != null
                && CobrowseSessionImplementation.TryCreate(session) is ISession s)
            {
                sessionDidUpdate(this, s);
                return true;
            }
            return false;
        }

        /// <inheritdoc/>
        public event EventHandler<ISession>? SessionDidEnd;

        internal bool RaiseSessionDidEnd(Session session)
        {
            EventHandler<ISession>? sessionDidEnd = SessionDidEnd;
            if (sessionDidEnd != null
                && CobrowseSessionImplementation.TryCreate(session) is ISession s)
            {
                sessionDidEnd(this, s);
                return true;
            }
            return false;
        }

        /// <inheritdoc/>
        public ISession? CurrentSession
            => CobrowseSessionImplementation.TryCreate(NativeCobrowseIO.Instance.CurrentSession);

        /// <inheritdoc/>
        public void CreateSession(CobrowseCallback? callback)
        {
            NativeCobrowseIO.Instance.CreateSession((NSError e, Session session) =>
            {
                callback?.Invoke(e?.AsException(), CobrowseSessionImplementation.TryCreate(session));
            });
        }

        /// <inheritdoc/>
        public string Api
        {
            get => NativeCobrowseIO.Instance.Api;
            set => NativeCobrowseIO.Instance.Api = value;
        }

        /// <inheritdoc/>
        public string DeviceId => NativeCobrowseIO.Instance.DeviceId;

        /// <inheritdoc/>
        public string License
        {
            get => NativeCobrowseIO.Instance.License;
            set => NativeCobrowseIO.Instance.License = value;
        }

        /// <inheritdoc/>
        public void Start()
        {
            NativeCobrowseIO.Instance.SetDelegate(new CobrowseDelegateImplementation());
            NativeCobrowseIO.Instance.Start();
        }

        /// <inheritdoc/>
        public void Stop()
        {
            NativeCobrowseIO.Instance.Stop();
        }

        /// <inheritdoc/>
        public IReadOnlyDictionary<string, string> CustomData
        {
            get => NativeCobrowseIO.Instance.CustomData;
            set => NativeCobrowseIO.Instance.CustomData = value;
        }

        /// <inheritdoc/>
        public string[] Capabilities
        {
            get => NativeCobrowseIO.Instance.Capabilities;
            set => NativeCobrowseIO.Instance.Capabilities = value;
        }

        /// <inheritdoc/>
        public string[] WebViewRedactedViews
        {
            get => NativeCobrowseIO.Instance.WebViewRedactedViews;
            set => NativeCobrowseIO.Instance.WebViewRedactedViews = value;
        }

        /// <inheritdoc/>
        public void SetWebViewRedactedViews(string[] webviewRedactedViews, string forDomain)
            => NativeCobrowseIO.Instance.SetWebViewRedactedViews(webviewRedactedViews, forDomain);

        /// <inheritdoc/>
        public string[] GetWebViewRedactedViews(string forDomain)
            => NativeCobrowseIO.Instance.GetWebViewRedactedViews(forDomain);

        /// <inheritdoc/>
        public void SetWebViewUnredactedViews(string[] webviewUnredactedViews, string forDomain)
            => NativeCobrowseIO.Instance.SetWebViewUnredactedViews(webviewUnredactedViews, forDomain);

        /// <inheritdoc/>
        public string[] GetWebViewUnredactedViews(string forDomain)
            => NativeCobrowseIO.Instance.GetWebViewUnredactedViews(forDomain);

        /// <inheritdoc/>
        public bool Registration
        {
            get => NativeCobrowseIO.Instance.Registration;
            set => NativeCobrowseIO.Instance.Registration = value;
        }
    }
}
