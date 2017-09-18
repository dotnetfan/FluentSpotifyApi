using FluentSpotifyApi.Core.Client;

namespace FluentSpotifyApi.Client
{
    /// <summary>
    /// The HTTP client used for Spotify Web API Requests.
    /// </summary>
    /// <seealso cref="FluentSpotifyApi.Core.Client.TypedHttpClient" />
    /// <seealso cref="Client.ISpotifyHttpClient" />
    public class SpotifyHttpClient : TypedHttpClient, ISpotifyHttpClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyHttpClient"/> class.
        /// </summary>
        /// <param name="httpClientWrapper">The HTTP client wrapper.</param>
        public SpotifyHttpClient(IHttpClientWrapper httpClientWrapper) : base(httpClientWrapper)
        {
        }
    }
}
