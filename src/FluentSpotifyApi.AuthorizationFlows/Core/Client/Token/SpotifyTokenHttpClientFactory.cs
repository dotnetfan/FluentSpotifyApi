using System.Net.Http;
using FluentSpotifyApi.Core.Utils;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.Token
{
    internal class SpotifyTokenHttpClientFactory : ISpotifyTokenHttpClientFactory
    {
        private readonly IHttpClientFactory httpClientFactory;

        public SpotifyTokenHttpClientFactory(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public static string ClientName { get; } = SpotifyHttpUtils.GetClientName<ISpotifyTokenClient>();

        public HttpClient CreateClient() => this.httpClientFactory.CreateClient(ClientName);
    }
}
