using System;
using Microsoft.Extensions.DependencyInjection;

namespace FluentSpotifyApi.AuthorizationFlows.Native.AuthorizationCode.DependencyInjection
{
    internal class SpotifyAuthorizationCodeFlowCoreBuilder : ISpotifyAuthorizationCodeFlowCoreBuilder
    {
        private readonly IHttpClientBuilder tokenHttpClientBuilder;
        private readonly IHttpClientBuilder userHttpClientBuilder;

        public SpotifyAuthorizationCodeFlowCoreBuilder(IHttpClientBuilder tokenHttpClientBuilder, IHttpClientBuilder userHttpClientBuilder)
        {
            this.tokenHttpClientBuilder = tokenHttpClientBuilder;
            this.userHttpClientBuilder = userHttpClientBuilder;
        }

        public ISpotifyAuthorizationCodeFlowCoreBuilder ConfigureTokenHttpClientBuilder(Action<IHttpClientBuilder> configureBuilder)
        {
            configureBuilder(this.tokenHttpClientBuilder);
            return this;
        }

        public ISpotifyAuthorizationCodeFlowCoreBuilder ConfigureUserHttpClientBuilder(Action<IHttpClientBuilder> configureBuilder)
        {
            configureBuilder(this.userHttpClientBuilder);
            return this;
        }
    }
}