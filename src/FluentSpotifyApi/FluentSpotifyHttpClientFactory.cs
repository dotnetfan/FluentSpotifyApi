using System.Net.Http;
using FluentSpotifyApi.Core.Utils;

namespace FluentSpotifyApi
{
    internal class FluentSpotifyHttpClientFactory : IFluentSpotifyHttpClientFactory
    {
        private readonly IHttpClientFactory httpClientFactory;

        public FluentSpotifyHttpClientFactory(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public static string ClientName { get; } = SpotifyHttpUtils.GetClientName<IFluentSpotifyClient>();

        public HttpClient CreateClient() => this.httpClientFactory.CreateClient(ClientName);
    }
}
