using System.Net.Http;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.Token
{
    /// <summary>
    /// The HTTP Client factory for <see cref="ISpotifyTokenClient"/>.
    /// </summary>
    public interface ISpotifyTokenHttpClientFactory
    {
        /// <summary>
        /// Creates HTTP Client for <see cref="ISpotifyTokenClient"/>.
        /// </summary>
        /// <returns></returns>
        HttpClient CreateClient();
    }
}
