using System;
using Android.Runtime;
using Cobrowse.IO.Android;
using JError = Java.Lang.Error;
using NativeCobrowseIO = Cobrowse.IO.Android.CobrowseIO;

namespace Cobrowse.IO;

/// <summary>
/// Cross-platform wrapper of the Cobrowse.io session metrics.
/// </summary>
[Preserve(AllMembers = true)]
public class CobrowseSessionMetricsImplementation : ISessionMetrics
{
    private readonly Session.IMetrics _platformSessionMetrics;

    public CobrowseSessionMetricsImplementation(Session.IMetrics sessionMetrics)
    {
        _platformSessionMetrics = sessionMetrics ?? throw new ArgumentNullException(nameof(sessionMetrics));
    }

    public static CobrowseSessionMetricsImplementation TryCreate(Session.IMetrics sessionMetrics)
    {
        return sessionMetrics != null
            ? new CobrowseSessionMetricsImplementation(sessionMetrics)
            : null;
    }

    /// <inheritdoc/>
    public double Latency
    {
        get
        {
            float milliseconds = _platformSessionMetrics.Latency;
            return Convert.ToDouble(milliseconds) * 1000d;
        }
    }

    /// <inheritdoc/>
    public DateTime? LastAlive
    {
        get
        {
            Java.Util.Date? d = _platformSessionMetrics.LastAlive;
            return d.ToDateTime();
        }
    }
}
