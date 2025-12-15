namespace Cobrowse.IO;

/// <summary>
/// Describes the reason a Session ended.
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
