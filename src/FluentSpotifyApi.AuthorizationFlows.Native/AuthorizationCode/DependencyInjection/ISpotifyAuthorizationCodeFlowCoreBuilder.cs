using System;
using Microsoft.Extensions.DependencyInjection;

namespace FluentSpotifyApi.AuthorizationFlows.Native.AuthorizationCode.DependencyInjection
{
    /// <summary>
    /// A builder that can be used to configure the Spotify Authorization Code Flow for native apps.
    /// </summary>
    public interface ISpotifyAuthorizationCodeFlowCoreBuilder
    {
        /// <summary>
        /// Configures HTTP client builder for <see cref="Core.Client.Token.ISpotifyTokenClient"/> HTTP client.
        /// </summary>
        /// <param name="configureBuilder">Used to configure the <see cref="IHttpClientBuilder"/>.</param>
        ISpotifyAuthorizationCodeFlowCoreBuilder ConfigureTokenHttpClientBuilder(Action<IHttpClientBuilder> configureBuilder);

        /// <summary>
        /// Configures HTTP client builder for <see cref="Core.Client.User.ISpotifyUserClient"/> HTTP client.
        /// </summary>
        /// <param name="configureBuilder">Used to configure the <see cref="IHttpClientBuilder"/>.</param>
        ISpotifyAuthorizationCodeFlowCoreBuilder ConfigureUserHttpClientBuilder(Action<IHttpClientBuilder> configureBuilder);
    }
}
