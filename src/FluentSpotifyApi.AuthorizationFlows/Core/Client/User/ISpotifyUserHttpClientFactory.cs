using System.Net.Http;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.User
{
    /// <summary>
    /// The HTTP Client factory for <see cref="ISpotifyUserClient"/>.
    /// </summary>
    public interface ISpotifyUserHttpClientFactory
    {
        /// <summary>
        /// Creates HTTP Client for <see cref="ISpotifyUserClient"/>.
        /// </summary>
        /// <returns></returns>
        HttpClient CreateClient();
    }
}
