using System;
using System.Collections.Generic;

namespace Cobrowse.IO
{
    /// <summary>
    /// Cross-platform wrapper of the Cobrowse.io session.
    /// </summary>
    public interface ISession
    {
        /// <summary>
        /// Gets the session's code.
        /// </summary>
        string? Code { get; }

        /// <summary>
        /// Gets the session's state.
        /// </summary>
        string State { get; }

        /// <summary>
        /// Gets a value indicating if the session running, frames are streaming to the agent.
        /// </summary>
        bool IsActive { get; }

        /// <summary>
        /// Gets a value indicating if waiting for the user to confirm the session.
        /// </summary>
        bool IsAuthorizing { get; }

        /// <summary>
        /// Gets a value indicating if the ession is over and can no longer be used or edited.
        /// </summary>
        bool IsEnded { get; }

        /// <summary>
        /// Gets a value indicating if the session has been created but is waiting for agent or user.
        /// </summary>
        bool IsPending { get; }

        /// <summary>
        /// Gets a value indicating if an agent object is available.
        /// </summary>
        bool HasAgent { get; }

        /// <summary>
        /// Gets an agent instance.
        /// </summary>
        IAgent? Agent { get; }

        /// <summary>
        /// Returns the network metrics for the session.
        /// </summary>
        ISessionMetrics? Metrics { get; }

        /// <summary>
        /// Gets the current remote control status.
        /// </summary>
        RemoteControlState RemoteControl { get; }

        /// <summary>
        /// Enables or disables remote control.
        /// </summary>
        void SetRemoteControl(RemoteControlState value, CobrowseCallback? callback);

        /// <summary>
        /// Gets the current full device status.
        /// </summary>
        FullDeviceState FullDevice { get; }

        /// <summary>
        /// Enable or disables full device.
        /// </summary>
        void SetFullDevice(FullDeviceState value, CobrowseCallback? callback);

        /// <summary>
        /// Sets the capabilities on this session object.
        /// </summary>
        void SetCapabilities(string[] capabilities, CobrowseCallback? callback);

        /// <summary>
        /// Activates the session.
        /// </summary>
        void Activate(CobrowseCallback? callback);

        /// <summary>
        /// Ends the session.
        /// </summary>
        void End(CobrowseCallback? callback);

        /// <summary>
        /// Returns an immutable dictionary representing custom data of the session instance.
        /// </summary>
        IReadOnlyDictionary<string, string> CustomData { get; }

        /// <summary>
        /// Sets custom data on the session instance.
        /// </summary>
        void SetCustomData(
            IReadOnlyDictionary<string, string> customData,
            CobrowseCallback? callback);

        /// <summary>
        /// The reason the session ended
        /// </summary>
        SessionEndedReason EndedReason { get; }

        /// <summary>
        /// When the session was created
        /// </summary>
        DateTime? Created { get; }

        /// <summary>
        /// When the session will expire
        /// </summary>
        DateTime? Expires { get; }

        /// <summary>
        /// When the session was activated
        /// </summary>
        DateTime? Activated { get; }

        /// <summary>
        /// When the session was last updated
        /// </summary>
        DateTime? Updated { get; }

        /// <summary>
        /// When the session was ended
        /// </summary>
        DateTime? Ended { get; }
    }
}
