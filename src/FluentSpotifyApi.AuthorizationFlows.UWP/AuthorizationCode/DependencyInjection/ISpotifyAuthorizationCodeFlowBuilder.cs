using System;
using Microsoft.Extensions.DependencyInjection;

namespace FluentSpotifyApi.AuthorizationFlows.UWP.AuthorizationCode.DependencyInjection
{
    /// <summary>
    /// A builder that can be used to configure the Spotify Authorization Code Flow for UWP apps.
    /// </summary>
    public interface ISpotifyAuthorizationCodeFlowBuilder
    {
        /// <summary>
        /// Configures HTTP client builder for <see cref="AuthorizationFlows.Core.Client.Token.IAccessTokenResponse"/> HTTP client.
        /// </summary>
        /// <param name="configureBuilder">Used to configure the <see cref="IHttpClientBuilder"/>.</param>
        /// <returns></returns>
        ISpotifyAuthorizationCodeFlowBuilder ConfigureTokenHttpClientBuilder(Action<IHttpClientBuilder> configureBuilder);

        /// <summary>
        /// Configures HTTP client builder for <see cref="AuthorizationFlows.Core.Client.User.ISpotifyUserClient"/> HTTP client.
        /// </summary>
        /// <param name="configureBuilder">Used to configure the <see cref="IHttpClientBuilder"/>.</param>
        /// <returns></returns>
        ISpotifyAuthorizationCodeFlowBuilder ConfigureUserHttpClientBuilder(Action<IHttpClientBuilder> configureBuilder);
    }
}
