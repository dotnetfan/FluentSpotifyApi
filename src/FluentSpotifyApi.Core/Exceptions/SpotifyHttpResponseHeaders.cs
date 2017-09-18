using System;
using System.Net.Http.Headers;

namespace FluentSpotifyApi.Core.Exceptions
{
    /// <summary>
    /// Spotify HTTP Response headers.
    /// </summary>
    public class SpotifyHttpResponseHeaders
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyHttpResponseHeaders"/> class.
        /// </summary>
        /// <param name="httpResponseHeaders">The HTTP response headers.</param>
        public SpotifyHttpResponseHeaders(HttpResponseHeaders httpResponseHeaders)
        {
            this.RetryAfter = httpResponseHeaders?.RetryAfter?.Delta;
        }

        /// <summary>
        /// Gets the retry after.
        /// </summary>
        /// <value>
        /// The retry after.
        /// </value>
        public TimeSpan? RetryAfter { get; private set; }
    }
}
