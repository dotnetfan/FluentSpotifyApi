using FluentSpotifyApi.AuthorizationFlows.Core.Client.Token.Handlers;
using FluentSpotifyApi.Core.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.Token.DependencyInjection
{
    /// <summary>
    /// The extension methods for registering <see cref="ISpotifyTokenClient"/> implementation in an <see cref="IServiceCollection"/>.
    /// </summary>
    public static class SpotifyTokenClientServiceCollectionExtensions
    {
        /// <summary>
        /// Adds <see cref="ISpotifyTokenClient"/> implementation to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/>.</param>
        public static IHttpClientBuilder AddSpotifyTokenClient(this IServiceCollection services)
        {
            services.TryAddSingleton<ISpotifyTokenHttpClientFactory, SpotifyTokenHttpClientFactory>();
            services.TryAddTransient<SpotifyAuthenticationErrorHandler>();
            var httpClientBuilder = services.AddHttpClient(SpotifyTokenHttpClientFactory.ClientName)
                .ConfigureHttpClient((sp, client) =>
                {
                    var optionsProvider = sp.GetRequiredService<IOptionsProvider<ISpotifyTokenClientOptions>>();
                    client.BaseAddress = optionsProvider.Get().TokenEndpoint;
                })
                .AddHttpMessageHandler<SpotifyAuthenticationErrorHandler>();

            services.TryAddSingleton<ISpotifyTokenClient, SpotifyTokenClient>();

            return httpClientBuilder;
        }
    }
}
