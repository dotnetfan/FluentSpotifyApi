using System;
using Microsoft.Extensions.DependencyInjection;

namespace FluentSpotifyApi.AuthorizationFlows.ClientCredentials.DependencyInjection
{
    internal class SpotifyClientCredentialsFlowBuilder : ISpotifyClientCredentialsFlowBuilder
    {
        private readonly IHttpClientBuilder tokenHttpClientBuilder;

        public SpotifyClientCredentialsFlowBuilder(IHttpClientBuilder tokenHttpClientBuilder)
        {
            this.tokenHttpClientBuilder = tokenHttpClientBuilder;
        }

        public ISpotifyClientCredentialsFlowBuilder ConfigureTokenHttpClientBuilder(Action<IHttpClientBuilder> configureBuilder)
        {
            configureBuilder(this.tokenHttpClientBuilder);
            return this;
        }
    }
}