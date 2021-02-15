using System;
using Microsoft.Extensions.DependencyInjection;

namespace FluentSpotifyApi.AuthorizationFlows.AspNetCore.AuthorizationCode.DependencyInjection
{
    internal class SpotifyAuthorizationCodeFlowBuilder : ISpotifyAuthorizationCodeFlowBuilder
    {
        private readonly IHttpClientBuilder tokenHttpClientBuilder;

        public SpotifyAuthorizationCodeFlowBuilder(IHttpClientBuilder tokenHttpClientBuilder)
        {
            this.tokenHttpClientBuilder = tokenHttpClientBuilder;
        }

        public ISpotifyAuthorizationCodeFlowBuilder ConfigureTokenHttpClientBuilder(Action<IHttpClientBuilder> configureBuilder)
        {
            configureBuilder(this.tokenHttpClientBuilder);
            return this;
        }
    }
}