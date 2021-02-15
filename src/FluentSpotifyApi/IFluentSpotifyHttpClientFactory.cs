using System.Net.Http;

namespace FluentSpotifyApi
{
    /// <summary>
    /// The HTTP Client factory for <see cref="IFluentSpotifyClient"/>.
    /// </summary>
    public interface IFluentSpotifyHttpClientFactory
    {
        /// <summary>
        /// Creates HTTP Client for <see cref="IFluentSpotifyClient"/>.
        /// </summary>
        /// <returns></returns>
        HttpClient CreateClient();
    }
}
