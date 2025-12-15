using System;
using Foundation;
using Cobrowse.IO.iOS;

namespace Cobrowse.IO
{
    /// <summary>
    /// Cross-platform wrapper of the Cobrowse.io Agent class.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class AgentImplementation : IAgent
    {
        private Agent _platformAgent;

        public AgentImplementation(Agent agent)
        {
            _platformAgent = agent ?? throw new ArgumentNullException(nameof(agent));
        }

        /// <inheritdoc/>
        public string Email => _platformAgent.Email;

        /// <inheritdoc/>
        public string Id => _platformAgent.Id;

        /// <inheritdoc/>
        public string Name => _platformAgent.Name;
    }
}
