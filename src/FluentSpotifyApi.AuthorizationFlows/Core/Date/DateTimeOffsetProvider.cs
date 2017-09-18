using System;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Date
{
    /// <summary>
    /// Provides methods for getting current <see cref="DateTimeOffset"/>.
    /// </summary>
    /// <seealso cref="IDateTimeOffsetProvider" />
    public class DateTimeOffsetProvider : IDateTimeOffsetProvider
    {
        /// <summary>
        /// Gets the UTC now.
        /// </summary>
        /// <returns></returns>
        public DateTimeOffset GetUtcNow()
        {
            return DateTime.UtcNow;
        }
    }
}
