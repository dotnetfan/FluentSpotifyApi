using System;
using FluentSpotifyApi.AuthorizationFlows.Core;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.Token;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.Token.DependencyInjection;
using FluentSpotifyApi.AuthorizationFlows.Core.DependencyInjection;
using FluentSpotifyApi.Core.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FluentSpotifyApi.AuthorizationFlows.ClientCredentials.DependencyInjection
{
    /// <summary>
    /// Extension methods for configuring Spotify Client Credentials Flow as part of <see cref="System.Net.Http.HttpClient"/> message handler pipeline
    /// using an <see cref="IHttpClientBuilder"/>.
    /// </summary>
    public static class SpotifyClientCredentialsFlowHttpClientBuilderExtensions
    {
        /// <summary>
        /// Adds Spotify Client Credentials Flow message handler and required services to the specified <see cref="IHttpClientBuilder"/>.
        /// </summary>
        /// <param name="httpClientBuilder">The <see cref="IHttpClientBuilder"/>.</param>
        public static ISpotifyClientCredentialsFlowBuilder AddSpotifyClientCredentialsFlow(this IHttpClientBuilder httpClientBuilder)
        {
            return httpClientBuilder.AddSpotifyClientCredentialsFlow(o => { });
        }

        /// <summary>
        /// Adds Spotify Client Credentials Flow message handler and required services to the specified <see cref="IHttpClientBuilder"/>.
        /// </summary>
        /// <param name="httpClientBuilder">The <see cref="IHttpClientBuilder"/>.</param>
        /// <param name="configureOptions">Used to configure the <see cref="SpotifyClientCredentialsFlowOptions"/>.</param>
        public static ISpotifyClientCredentialsFlowBuilder AddSpotifyClientCredentialsFlow(this IHttpClientBuilder httpClientBuilder, Action<SpotifyClientCredentialsFlowOptions> configureOptions)
        {
            httpClientBuilder.Services
                .AddOptions<SpotifyClientCredentialsFlowOptions>().Validate(o =>
                {
                    o.Validate();
                    return true;
                })
                .Configure(configureOptions);

            httpClientBuilder.Services.TryAddSingleton<IAuthenticationTicketProvider, AuthenticationTicketProvider>();
            httpClientBuilder.AddSpotifyAuthorizationFlowsCore();

            httpClientBuilder.Services.TryAddSingleton<IOptionsProvider<ISpotifyTokenClientOptions>, OptionsProvider<SpotifyClientCredentialsFlowOptions>>();
            var tokenHttpClientBuilder = httpClientBuilder.Services.AddSpotifyTokenClient();

            var builder = new SpotifyClientCredentialsFlowBuilder(tokenHttpClientBuilder);
            return builder;
        }
    }
}
