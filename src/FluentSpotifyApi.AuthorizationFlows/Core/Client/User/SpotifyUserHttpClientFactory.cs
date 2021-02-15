using System.Net.Http;
using FluentSpotifyApi.Core.Utils;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.User
{
    internal class SpotifyUserHttpClientFactory : ISpotifyUserHttpClientFactory
    {
        private readonly IHttpClientFactory httpClientFactory;

        public SpotifyUserHttpClientFactory(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public static string ClientName { get; } = SpotifyHttpUtils.GetClientName<ISpotifyUserClient>();

        public HttpClient CreateClient() => this.httpClientFactory.CreateClient(ClientName);
    }
}
