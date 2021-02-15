using System;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.Authorization;
using FluentSpotifyApi.AuthorizationFlows.Native.AuthorizationCode;
using FluentSpotifyApi.AuthorizationFlows.Native.AuthorizationCode.DependencyInjection;
using FluentSpotifyApi.AuthorizationFlows.UWP.Core.Client.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FluentSpotifyApi.AuthorizationFlows.UWP.AuthorizationCode.DependencyInjection
{
    /// <summary>
    /// Extension methods for configuring Spotify Authorization Code Flow for UWP apps as part of <see cref="System.Net.Http.HttpClient"/> message handler pipeline
    /// using an <see cref="IHttpClientBuilder"/>.
    /// </summary>
    public static class SpotifyAuthorizationCodeFlowHttpClientBuilderExtensions
    {
        /// <summary>
        /// Adds Spotify Authorization Code Flow message handler for UWP apps, implementation of <see cref="IAuthenticationManager"/> and other required services to the specified <see cref="IHttpClientBuilder"/>.
        /// </summary>
        /// <param name="httpClientBuilder">The <see cref="IHttpClientBuilder"/>.</param>
        /// <returns></returns>
        public static ISpotifyAuthorizationCodeFlowBuilder AddSpotifyAuthorizationCodeFlow(this IHttpClientBuilder httpClientBuilder)
        {
            return httpClientBuilder.AddSpotifyAuthorizationCodeFlow(o => { });
        }

        /// <summary>
        /// Adds Spotify Authorization Code Flow message handler for UWP apps, implementation of <see cref="IAuthenticationManager"/> and other required services to the specified <see cref="IHttpClientBuilder"/>.
        /// </summary>
        /// <param name="httpClientBuilder">The <see cref="IHttpClientBuilder"/>.</param>
        /// <param name="configureOptions">Used to configure the <see cref="SpotifyAuthorizationCodeFlowOptions"/>.</param>
        /// <returns></returns>
        public static ISpotifyAuthorizationCodeFlowBuilder AddSpotifyAuthorizationCodeFlow(this IHttpClientBuilder httpClientBuilder, Action<SpotifyAuthorizationCodeFlowOptions> configureOptions)
        {
            httpClientBuilder.Services.TryAddSingleton<IAuthorizationRedirectUriProvider, AuthorizationRedirectUriProvider>();
            httpClientBuilder.Services.TryAddSingleton<IAuthorizationInteractionClient, AuthorizationInteractionClient>();
            httpClientBuilder.Services.TryAddSingleton<IAuthenticationTicketStorage, AuthenticationTicketStorage>();

            var coreBuilder = httpClientBuilder.AddSpotifyAuthorizationCodeFlowCore(configureOptions);

            return new SpotifyAuthorizationCodeFlowBuilder(coreBuilder);
        }
    }
}
