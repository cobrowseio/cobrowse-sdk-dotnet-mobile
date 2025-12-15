namespace Cobrowse.IO;

/// <summary>
/// Describes the reason a <see cref="ISession"/> ended.
/// </summary>
public enum SessionEndedReason
{
    Unknown,
    DeviceEnded,
    AgentEnded,
    PendingTimeout,
    AuthorizingTimeout,
    ActiveTimeout,
    LimitEnforcement,
}
