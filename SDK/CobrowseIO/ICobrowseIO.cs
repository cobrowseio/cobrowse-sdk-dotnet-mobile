﻿using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Cobrowse.IO
{
    /// <summary>
    /// Cross-platform wrapper of the Cobrowse.io SDK.
    /// </summary>
    public interface ICobrowseIO
    {
        /// <summary>
        /// Occurs when a session is first made available to the device,
        /// whether by creating a 6 digit code, or via a connect request from an agent.
        /// </summary>
        event EventHandler<ISession> SessionDidLoad;

        /// <summary>
        /// Occurs when a session is requested.
        /// </summary>
        event EventHandler<ISession> SessionDidRequest;

        /// <summary>
        /// Occurs when a session is updated.
        /// </summary>
        event EventHandler<ISession> SessionDidUpdate;

        /// <summary>
        /// Occurs when a session ends.
        /// </summary>
        event EventHandler<ISession> SessionDidEnd;

        /// <summary>
        /// Occurs when an agent requests remote control.
        /// </summary>
        event EventHandler<ISession> RemoteControlRequest;

        /// <summary>
        /// Returns the current session instance or null if it doesn't exist.
        /// </summary>
        ISession? CurrentSession { get; }

        /// <summary>
        /// Creates a new Cobrowse.io session.
        /// </summary>
        void CreateSession(CobrowseCallback? callback);

        /// <summary>
        /// Gets or sets the API string.
        /// </summary>
        string Api { get; set; }

        /// <summary>
        /// Gets the current Cobrowse.io device ID.
        /// </summary>
        string DeviceId { get; }

        /// <summary>
        /// Gets or sets the license.
        /// </summary>
        string License { get; set; }

        /// <summary>
        /// Starts the Cobrowse.io.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops the Cobrowse.io.
        /// </summary>
        void Stop();

        /// <summary>
        /// Gets or sets Cobrowse.io custom data.
        /// </summary>
        IReadOnlyDictionary<string, string> CustomData { get; set; }

        /// <summary>
        /// Gets or sets the available capabilities for a session. Different
        /// annotation tools and events will be available during a session
        /// depending on the capabilities you set here. By default all
        /// capabilities supported by the device are enabled.
        /// </summary>
        string[] Capabilities { get; set; }

        /// <summary>
        /// Gets or sets the CSS selectors which will be used to redact content within WebViews.
        /// Any HTML element matching one of the selectors configured here will be redacted and
        /// not visible to your agents.
        /// Defaults to an empty list which means the feature is disabled.
        /// </summary>
        string[] WebViewRedactedViews { get; set; }

        /// <summary>
        /// By default, when the SDK starts it will register the device to your account and share
        /// its connectivity state. This provides the dashboard with a list of devices which are
        /// online and ready to connect. <p>
        /// If you don't need to see a list of devices in your dashboard, e.g. your sessions start
        /// only using <see cref="ICobrowseIO.CreateSession(CobrowseCallback)"/>,
        /// then you can stop the SDK from registering the device and its status by passing the
        /// <c>registration</c> option with a value of <c>false</c>.
        /// </summary>
        bool Registration { get; set; }
    }
}
