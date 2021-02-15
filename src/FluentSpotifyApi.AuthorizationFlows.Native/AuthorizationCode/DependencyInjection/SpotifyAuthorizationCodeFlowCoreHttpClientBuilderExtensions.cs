using System;
using FluentSpotifyApi.AuthorizationFlows.Core;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.Authorization;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.Authorization.DependencyInjection;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.Token;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.Token.DependencyInjection;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.User;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.User.DependencyInjection;
using FluentSpotifyApi.AuthorizationFlows.Core.DependencyInjection;
using FluentSpotifyApi.Core.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FluentSpotifyApi.AuthorizationFlows.Native.AuthorizationCode.DependencyInjection
{
    /// <summary>
    /// Extension methods for configuring Spotify Authorization Code Flow for native apps as part of <see cref="System.Net.Http.HttpClient"/> message handler pipeline
    /// using an <see cref="IHttpClientBuilder"/>.
    /// </summary>
    public static class SpotifyAuthorizationCodeFlowCoreHttpClientBuilderExtensions
    {
        /// <summary>
        /// Adds Spotify Authorization Code Flow message handler for native apps, implementation of <see cref="IAuthenticationManager"/> and other required services to the specified <see cref="IHttpClientBuilder"/>.
        /// </summary>
        /// <typeparam name="TOptions">The options type.</typeparam>
        /// <param name="httpClientBuilder">The <see cref="IHttpClientBuilder"/>.</param>
        /// <param name="configureOptions">Used to configure the <typeparamref name="TOptions"/>.</param>
        /// <returns></returns>
        public static ISpotifyAuthorizationCodeFlowCoreBuilder AddSpotifyAuthorizationCodeFlowCore<TOptions>(this IHttpClientBuilder httpClientBuilder, Action<TOptions> configureOptions)
            where TOptions : SpotifyAuthorizationCodeFlowOptions, new()
        {
            httpClientBuilder.Services
                .AddOptions<TOptions>().Validate(o =>
                {
                    o.Validate();
                    return true;
                })
                .Configure(configureOptions);

            httpClientBuilder.Services.TryAddSingleton<IOptionsProvider<TOptions>, OptionsProvider<TOptions>>();
            httpClientBuilder.Services.TryAddSingleton<IOptionsProvider<SpotifyAuthorizationCodeFlowOptions>>(sp => sp.GetRequiredService<IOptionsProvider<TOptions>>());
            httpClientBuilder.Services.TryAddSingleton<IAuthenticationTicketRepository, AuthenticationTicketRepository>();
            httpClientBuilder.Services.TryAddSingleton<AuthenticationTicketProvider>();
            httpClientBuilder.Services.TryAddSingleton<IAuthenticationTicketProvider>(sp => sp.GetRequiredService<AuthenticationTicketProvider>());
            httpClientBuilder.Services.TryAddSingleton<IAuthenticationManager>(sp => sp.GetRequiredService<AuthenticationTicketProvider>());
            httpClientBuilder.AddSpotifyAuthorizationFlowsCore();

            httpClientBuilder.Services.TryAddSingleton<IOptionsProvider<ISpotifyAuthorizationClientOptions>>(sp => sp.GetRequiredService<IOptionsProvider<TOptions>>());
            httpClientBuilder.Services.AddSpotifyAuthorizationClient();

            httpClientBuilder.Services.TryAddSingleton<IOptionsProvider<ISpotifyTokenClientOptions>>(sp => sp.GetRequiredService<IOptionsProvider<TOptions>>());
            var tokenHttpClientBuilder = httpClientBuilder.Services.AddSpotifyTokenClient();

            httpClientBuilder.Services.TryAddSingleton<IOptionsProvider<ISpotifyUserClientOptions>>(sp => sp.GetRequiredService<IOptionsProvider<TOptions>>());
            var userHttpClientBuilder = httpClientBuilder.Services.AddSpotifyUserClient();

            return new SpotifyAuthorizationCodeFlowCoreBuilder(tokenHttpClientBuilder, userHttpClientBuilder);
        }
    }
}
