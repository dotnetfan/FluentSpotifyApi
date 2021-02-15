using System;
using FluentSpotifyApi.AuthorizationFlows.Native.AuthorizationCode.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace FluentSpotifyApi.AuthorizationFlows.UWP.AuthorizationCode.DependencyInjection
{
    internal class SpotifyAuthorizationCodeFlowBuilder : ISpotifyAuthorizationCodeFlowBuilder
    {
        private readonly ISpotifyAuthorizationCodeFlowCoreBuilder coreBuilder;

        public SpotifyAuthorizationCodeFlowBuilder(ISpotifyAuthorizationCodeFlowCoreBuilder coreBuilder)
        {
            this.coreBuilder = coreBuilder;
        }

        public ISpotifyAuthorizationCodeFlowBuilder ConfigureTokenHttpClientBuilder(Action<IHttpClientBuilder> configureBuilder)
        {
            this.coreBuilder.ConfigureTokenHttpClientBuilder(configureBuilder);
            return this;
        }

        public ISpotifyAuthorizationCodeFlowBuilder ConfigureUserHttpClientBuilder(Action<IHttpClientBuilder> configureBuilder)
        {
            this.coreBuilder.ConfigureUserHttpClientBuilder(configureBuilder);
            return this;
        }
    }
}
