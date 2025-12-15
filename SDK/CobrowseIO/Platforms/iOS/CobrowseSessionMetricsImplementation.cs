using System;
using Foundation;
using Cobrowse.IO.iOS;
using NativeRemoteControlState = Cobrowse.IO.iOS.RemoteControlState;
using NativeFullDeviceState = Cobrowse.IO.iOS.FullDeviceState;

namespace Cobrowse.IO;

/// <summary>
/// Cross-platform wrapper of the Cobrowse.io session metrics.
/// </summary>
[Preserve(AllMembers = true)]
public class CobrowseSessionMetricsImplementation : ISessionMetrics
{
    private readonly SessionMetrics _platformSessionMetrics;

    public CobrowseSessionMetricsImplementation(SessionMetrics sessionMetrics)
    {
        _platformSessionMetrics = sessionMetrics ?? throw new ArgumentNullException(nameof(sessionMetrics));
    }

    public static CobrowseSessionMetricsImplementation TryCreate(SessionMetrics sessionMetrics)
    {
        return sessionMetrics != null
            ? new CobrowseSessionMetricsImplementation(sessionMetrics)
            : null;
    }

    /// <inheritdoc/>
    public double Latency => _platformSessionMetrics.Latency;

    /// <inheritdoc/>
    public DateTime? LastAlive
    {
        get
        {
            NSDate? d = _platformSessionMetrics.LastAlive;
            return d != null ? (DateTime)d : null;
        }
    }
}
