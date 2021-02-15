using System;
using Microsoft.Extensions.DependencyInjection;

namespace FluentSpotifyApi.DependencyInjection
{
    internal class FluentSpotifyClientBuilder : IFluentSpotifyClientBuilder
    {
        private readonly IHttpClientBuilder httpClientBuilder;

        public FluentSpotifyClientBuilder(IHttpClientBuilder httpClientBuilder)
        {
            this.httpClientBuilder = httpClientBuilder;
        }

        public IFluentSpotifyClientBuilder ConfigureHttpClientBuilder(Action<IHttpClientBuilder> configureBuilder)
        {
            configureBuilder(this.httpClientBuilder);
            return this;
        }
    }
}