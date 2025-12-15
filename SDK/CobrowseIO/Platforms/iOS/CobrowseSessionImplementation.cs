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

        /// <inheritdoc/>
        public string? Code => _platformSession.Code;

        /// <inheritdoc/>
        public string State => _platformSession.State;

        /// <inheritdoc/>
        public bool IsActive => _platformSession.IsActive;

        /// <inheritdoc/>
        public bool IsAuthorizing => _platformSession.IsAuthorizing;

        /// <inheritdoc/>
        public bool IsEnded => _platformSession.IsEnded;

        /// <inheritdoc/>
        public bool IsPending => _platformSession.IsPending;

        /// <inheritdoc/>
        public bool HasAgent => _platformSession.HasAgent;

        /// <inheritdoc/>
        public IAgent? Agent => _platformSession.Agent != null
            ? new AgentImplementation(_platformSession.Agent)
            : null;

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public void Activate(CobrowseCallback? callback)
        {
            _platformSession.Activate((NSError e, Session session) =>
            {
                callback?.Invoke(e?.AsException(), CobrowseSessionImplementation.TryCreate(session));
            });
        }

        /// <inheritdoc/>
        public void End(CobrowseCallback callback)
        {
            _platformSession.End((NSError e, Session session) =>
            {
                callback?.Invoke(e?.AsException(), CobrowseSessionImplementation.TryCreate(session));
            });
        }

        /// <inheritdoc/>
        public IReadOnlyDictionary<string, string> CustomData
            => _platformSession.CustomData;

        /// <inheritdoc/>
        public void SetCustomData(
            IReadOnlyDictionary<string, string> customData,
            CobrowseCallback? callback)
        {
            _platformSession.SetCustomData(customData, (NSError e, Session session) =>
            {
                callback?.Invoke(e?.AsException(), CobrowseSessionImplementation.TryCreate(session));
            });
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public DateTime? Created => (DateTime)_platformSession.Created;

        /// <inheritdoc/>
        public DateTime? Expires => (DateTime?)_platformSession.Expires;

        /// <inheritdoc/>
        public DateTime? Activated => (DateTime?)_platformSession.Activated;

        /// <inheritdoc/>
        public DateTime? Updated => (DateTime?)_platformSession.Updated;

        /// <inheritdoc/>
        public DateTime? Ended => (DateTime?)_platformSession.Ended;
    }
}
