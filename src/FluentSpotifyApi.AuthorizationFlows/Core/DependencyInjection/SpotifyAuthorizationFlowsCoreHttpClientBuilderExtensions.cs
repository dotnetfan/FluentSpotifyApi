using FluentSpotifyApi.AuthorizationFlows.Core.Time;
using FluentSpotifyApi.Core.User;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FluentSpotifyApi.AuthorizationFlows.Core.DependencyInjection
{
    /// <summary>
    /// Extension methods for configuring core parts of Spotify Authorization Flows as part of <see cref="System.Net.Http.HttpClient"/> message handler pipeline
    /// using an <see cref="IHttpClientBuilder"/>.
    /// </summary>
    public static class SpotifyAuthorizationFlowsCoreHttpClientBuilderExtensions
    {
        /// <summary>
        /// Adds core Spotify Authorization Flow message handler and required services to the specified <see cref="IHttpClientBuilder"/>.
        /// </summary>
        /// <param name="httpClientBuilder">The <see cref="IHttpClientBuilder"/>.</param>
        public static IHttpClientBuilder AddSpotifyAuthorizationFlowsCore(this IHttpClientBuilder httpClientBuilder)
        {
            httpClientBuilder.Services.TryAddSingleton<IClock, Clock>();
            httpClientBuilder.Services.TryAddSingleton<ICurrentUserProvider, CurrentUserProvider>();
            httpClientBuilder.Services.TryAddTransient<AuthenticationHandler>();
            httpClientBuilder.AddHttpMessageHandler<AuthenticationHandler>();

            return httpClientBuilder;
        }
    }
}
