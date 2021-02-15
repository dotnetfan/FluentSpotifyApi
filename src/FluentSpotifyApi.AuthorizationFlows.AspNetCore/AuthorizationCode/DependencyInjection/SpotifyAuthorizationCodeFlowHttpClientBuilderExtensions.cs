using FluentSpotifyApi.AuthorizationFlows.Core;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.Token;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.Token.DependencyInjection;
using FluentSpotifyApi.AuthorizationFlows.Core.DependencyInjection;
using FluentSpotifyApi.AuthorizationFlows.Core.Time;
using FluentSpotifyApi.Core.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FluentSpotifyApi.AuthorizationFlows.AspNetCore.AuthorizationCode.DependencyInjection
{
    /// <summary>
    /// Extension methods for configuring Spotify Authorization Code Flow for AspNetCore apps as part of <see cref="System.Net.Http.HttpClient"/> message handler pipeline
    /// using an <see cref="IHttpClientBuilder"/>. User authorization is performed via separate AspNetCore OAuth handler that needs to be added to the authentication pipeline
    /// using <see cref="Handler.SpotifyExtensions"/>.
    /// </summary>
    public static class SpotifyAuthorizationCodeFlowHttpClientBuilderExtensions
    {
        /// <summary>
        /// Adds Spotify Authorization Code Flow message handler for AspNetCore apps and other required services to the specified <see cref="IHttpClientBuilder"/>.
        /// </summary>
        /// <param name="httpClientBuilder">The <see cref="IHttpClientBuilder"/>.</param>
        /// <param name="authenticationScheme">The authentication scheme.</param>
        /// <param name="spotifyAuthenticationScheme">The Spotify authentication scheme.</param>
        /// <returns></returns>
        public static ISpotifyAuthorizationCodeFlowBuilder AddSpotifyAuthorizationCodeFlow(
            this IHttpClientBuilder httpClientBuilder,
            string authenticationScheme = null,
            string spotifyAuthenticationScheme = null)
        {
            httpClientBuilder.Services.AddHttpContextAccessor();
            httpClientBuilder.Services.TryAddSingleton<IClock, ClockFromSystemClock>();
            httpClientBuilder.Services.TryAddSingleton<ISemaphoreProvider, SemaphoreProvider>();
            httpClientBuilder.Services.TryAddScoped<SemaphoreWrapper>();
            httpClientBuilder.Services.TryAddSingleton<IAuthenticationManager>(sp => new AuthenticationManager(sp.GetRequiredService<IHttpContextAccessor>(), authenticationScheme));
            httpClientBuilder.Services.TryAddSingleton<IAuthenticationTicketProvider, AuthenticationTicketProvider>();
            httpClientBuilder.AddSpotifyAuthorizationFlowsCore();

            httpClientBuilder.Services.TryAddSingleton<IOptionsProvider<ISpotifyTokenClientOptions>>(sp => new AuthorizationCodeFlowOptionsProvider(sp.GetRequiredService<IHttpContextAccessor>(), spotifyAuthenticationScheme));
            var tokenHttpClientBuilder = httpClientBuilder.Services.AddSpotifyTokenClient();

            var builder = new SpotifyAuthorizationCodeFlowBuilder(tokenHttpClientBuilder);
            return builder;
        }
    }
}
