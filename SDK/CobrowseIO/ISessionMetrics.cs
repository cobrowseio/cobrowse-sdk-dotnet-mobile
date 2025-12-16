using System;

namespace Cobrowse.IO;

/// <summary>
/// Represents the network metrics of a <see cref="ISession"/>.
/// </summary>
public interface ISessionMetrics
{
    /// <summary>
    /// Returns the measured session latency. This is the round-trip time from the device to the server, it doesn't indicate the latency of the agent.
    /// </summary>
    /// <returns>The session latency, in seconds.</returns>
    double Latency { get; }

    /// <summary>
    /// Returns the timestamp of the last alive message that the SDK sent in a response to a probe from agent.
    /// </summary>
    DateTime? LastAlive { get; }
}
