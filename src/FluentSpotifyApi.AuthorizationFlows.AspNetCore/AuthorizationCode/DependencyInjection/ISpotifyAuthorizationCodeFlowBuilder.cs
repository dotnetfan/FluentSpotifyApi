using System;
using Microsoft.Extensions.DependencyInjection;

namespace FluentSpotifyApi.AuthorizationFlows.AspNetCore.AuthorizationCode.DependencyInjection
{
    /// <summary>
    /// A builder that can be used to configure the Spotify Authorization Code Flow for AspNetCore apps.
    /// </summary>
    public interface ISpotifyAuthorizationCodeFlowBuilder
    {
        /// <summary>
        /// Configures HTTP client builder for <see cref="Core.Client.Token.ISpotifyTokenClient"/> HTTP client.
        /// </summary>
        /// <param name="configureBuilder">Used to configure the <see cref="IHttpClientBuilder"/>.</param>
        /// <returns></returns>
        ISpotifyAuthorizationCodeFlowBuilder ConfigureTokenHttpClientBuilder(Action<IHttpClientBuilder> configureBuilder);
    }
}
