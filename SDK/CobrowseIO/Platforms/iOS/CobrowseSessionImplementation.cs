using System;
using System.Collections.Generic;
using Foundation;
using Cobrowse.IO.iOS;
using NativeRemoteControlState = Cobrowse.IO.iOS.RemoteControlState;
using NativeFullDeviceState = Cobrowse.IO.iOS.FullDeviceState;
using NativeSessionEndedReason = Cobrowse.IO.iOS.SessionEndedReason;

namespace Cobrowse.IO
{
    /// <summary>
    /// Cross-platform wrapper of the Cobrowse.io session.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class CobrowseSessionImplementation : ISession
    {
        private readonly Session _platformSession;

        public CobrowseSessionImplementation(Session session)
        {
            _platformSession = session ?? throw new ArgumentNullException(nameof(session));
        }

        public static CobrowseSessionImplementation TryCreate(Session session)
        {
            return session != null
                ? new CobrowseSessionImplementation(session)
                : null;
        }

        /// <summary>
        /// Gets the session's code.
        /// </summary>
        public string? Code => _platformSession.Code;

        /// <summary>
        /// Gets the session's state.
        /// </summary>
        public string State => _platformSession.State;

        /// <summary>
        /// Gets a value indicating if the session running, frames are streaming to the agent.
        /// </summary>
        public bool IsActive => _platformSession.IsActive;

        /// <summary>
        /// Gets a value indicating if waiting for the user to confirm the session.
        /// </summary>
        public bool IsAuthorizing => _platformSession.IsAuthorizing;

        /// <summary>
        /// Gets a value indicating if the ession is over and can no longer be used or edited.
        /// </summary>
        public bool IsEnded => _platformSession.IsEnded;

        /// <summary>
        /// Gets a value indicating if the session has been created but is waiting for agent or user.
        /// </summary>
        public bool IsPending => _platformSession.IsPending;

        /// <summary>
        /// Gets a value indicating if an agent object is available.
        /// </summary>
        public bool HasAgent => _platformSession.HasAgent;

        /// <summary>
        /// Gets an agent instance.
        /// </summary>
        public IAgent? Agent => _platformSession.Agent != null
            ? new AgentImplementation(_platformSession.Agent)
            : null;

        /// <summary>
        /// Returns the network metrics for the session.
        /// </summary>
        public ISessionMetrics? Metrics
        {
            get
            {
                var platformMetrics = _platformSession.Metrics;
                return CobrowseSessionMetricsImplementation.TryCreate(platformMetrics);
            }
        }

        /// <inheritdoc/>
        public RemoteControlState RemoteControl
        {
            get
            {
                switch (_platformSession.RemoteControl)
                {
                    case NativeRemoteControlState.Off:
                        return RemoteControlState.Off;
                    case NativeRemoteControlState.Requested:
                        return RemoteControlState.Requested;
                    case NativeRemoteControlState.Rejected:
                        return RemoteControlState.Rejected;
                    case NativeRemoteControlState.On:
                        return RemoteControlState.On;
                    default:
                        return default;
                }
            }
        }

        /// <inheritdoc/>
        public void SetRemoteControl(RemoteControlState value, CobrowseCallback? callback)
        {
            NativeRemoteControlState toBeSet;
            switch (value)
            {
                case RemoteControlState.Off:
                    toBeSet = NativeRemoteControlState.Off;
                    break;
                case RemoteControlState.Requested:
                    toBeSet = NativeRemoteControlState.Requested;
                    break;
                case RemoteControlState.Rejected:
                    toBeSet = NativeRemoteControlState.Rejected;
                    break;
                case RemoteControlState.On:
                    toBeSet = NativeRemoteControlState.On;
                    break;
                default:
                    toBeSet = default;
                    break;
            }

            _platformSession.SetRemoteControl(toBeSet, (NSError e, Session session) =>
            {
                callback?.Invoke(e?.AsException(), CobrowseSessionImplementation.TryCreate(session));
            });
        }

        /// <inheritdoc/>
        public FullDeviceState FullDevice
        {
            get
            {
                switch (_platformSession.FullDevice)
                {
                    case NativeFullDeviceState.Off:
                        return FullDeviceState.Off;
                    case NativeFullDeviceState.Requested:
                        return FullDeviceState.Requested;
                    case NativeFullDeviceState.Rejected:
                        return FullDeviceState.Rejected;
                    case NativeFullDeviceState.On:
                        return FullDeviceState.On;
                    default:
                        return default;
                }
            }
        }

        /// <inheritdoc/>
        public void SetFullDevice(FullDeviceState state, CobrowseCallback? callback)
        {
            NativeFullDeviceState toBeSet;
            switch (state)
            {
                case FullDeviceState.Off:
                    toBeSet = NativeFullDeviceState.Off;
                    break;
                case FullDeviceState.Requested:
                    toBeSet = NativeFullDeviceState.Requested;
                    break;
                case FullDeviceState.Rejected:
                    toBeSet = NativeFullDeviceState.Rejected;
                    break;
                case FullDeviceState.On:
                    toBeSet = NativeFullDeviceState.On;
                    break;
                default:
                    toBeSet = default;
                    break;
            }

            _platformSession.SetFullDevice(toBeSet, (NSError e, Session session) =>
            {
                callback?.Invoke(e?.AsException(), CobrowseSessionImplementation.TryCreate(session));
            });
        }

        /// <inheritdoc/>
        public void SetCapabilities(string[] capabilities, CobrowseCallback callback)
        {
            _platformSession.SetCapabilities(capabilities, (NSError e, Session session) =>
            {
                callback?.Invoke(e?.AsException(), CobrowseSessionImplementation.TryCreate(session));
            });
        }

        /// <summary>
        /// Activates the session.
        /// </summary>
        public void Activate(CobrowseCallback? callback)
        {
            _platformSession.Activate((NSError e, Session session) =>
            {
                callback?.Invoke(e?.AsException(), CobrowseSessionImplementation.TryCreate(session));
            });
        }

        /// <summary>
        /// Ends the session.
        /// </summary>
        public void End(CobrowseCallback callback)
        {
            _platformSession.End((NSError e, Session session) =>
            {
                callback?.Invoke(e?.AsException(), CobrowseSessionImplementation.TryCreate(session));
            });
        }

        /// <summary>
        /// Returns an immutable dictionary representing custom data of the session instance.
        /// </summary>
        public IReadOnlyDictionary<string, string> CustomData
            => _platformSession.CustomData;

        /// <summary>
        /// Sets custom data on the session instance.
        /// </summary>
        public void SetCustomData(
            IReadOnlyDictionary<string, string> customData,
            CobrowseCallback? callback)
        {
            _platformSession.SetCustomData(customData, (NSError e, Session session) =>
            {
                callback?.Invoke(e?.AsException(), CobrowseSessionImplementation.TryCreate(session));
            });
        }

        /// <summary>
        /// The reason the session ended
        /// </summary>
        public SessionEndedReason EndedReason
        {
            get
            {
                switch (_platformSession.EndedReason)
                {
                    case NativeSessionEndedReason.Unknown:
                        return SessionEndedReason.Unknown;
                    case NativeSessionEndedReason.DeviceEnded:
                        return SessionEndedReason.DeviceEnded;
                    case NativeSessionEndedReason.AgentEnded:
                        return SessionEndedReason.AgentEnded;
                    case NativeSessionEndedReason.PendingTimeout:
                        return SessionEndedReason.PendingTimeout;
                    case NativeSessionEndedReason.AuthorizingTimeout:
                        return SessionEndedReason.AuthorizingTimeout;
                    case NativeSessionEndedReason.ActiveTimeout:
                        return SessionEndedReason.ActiveTimeout;
                    case NativeSessionEndedReason.LimitEnforcement:
                        return SessionEndedReason.LimitEnforcement;
                    default:
                        return default;
                }
            }
        }

        /// <summary>
        /// When the session was created
        /// </summary>
        public DateTime Created => (DateTime)_platformSession.Created;

        /// <summary>
        /// When the session will expire
        /// </summary>
        public DateTime? Expires => (DateTime?)_platformSession.Expires;

        /// <summary>
        /// When the session was activated
        /// </summary>
        public DateTime? Activated => (DateTime?)_platformSession.Activated;

        /// <summary>
        /// When the session was last updated
        /// </summary>
        public DateTime? Updated => (DateTime?)_platformSession.Updated;

        /// <summary>
        /// When the session was ended
        /// </summary>
        public DateTime? Ended => (DateTime?)_platformSession.Ended;
    }
}
