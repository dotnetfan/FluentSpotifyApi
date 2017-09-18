using System;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Date
{
    /// <summary>
    /// Provides methods for getting current <see cref="DateTimeOffset"/>.
    /// </summary>
    public interface IDateTimeOffsetProvider
    {
        /// <summary>
        /// Gets the UTC now.
        /// </summary>
        /// <returns></returns>
        DateTimeOffset GetUtcNow();
    }
}
