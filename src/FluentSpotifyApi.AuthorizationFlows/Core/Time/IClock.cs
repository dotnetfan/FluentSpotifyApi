using System;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Time
{
    /// <summary>
    /// The clock interface.
    /// </summary>
    public interface IClock
    {
        /// <summary>
        /// Gets the current system time in UTC.
        /// </summary>
        DateTimeOffset GetUtcNow();
    }
}
