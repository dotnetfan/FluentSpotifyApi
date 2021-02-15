using FluentSpotifyApi.Core.HttpHandlers;
using FluentSpotifyApi.Core.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.User.DependencyInjection
{
    /// <summary>
    /// The extension methods for registering <see cref="ISpotifyUserClient"/> implementation in an <see cref="IServiceCollection"/>.
    /// </summary>
    public static class SpotifyUserClientServiceCollectionExtensions
    {
        /// <summary>
        /// Adds <see cref="ISpotifyUserClient"/> implementation to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/>.</param>
        public static IHttpClientBuilder AddSpotifyUserClient(this IServiceCollection services)
        {
            services.TryAddSingleton<ISpotifyUserHttpClientFactory, SpotifyUserHttpClientFactory>();
            services.TryAddTransient<SpotifyRegularErrorHandler<ISpotifyUserClient>>();
            var httpClientBuilder = services.AddHttpClient(SpotifyUserHttpClientFactory.ClientName)
                .ConfigureHttpClient((sp, client) =>
                {
                    var optionsProvider = sp.GetRequiredService<IOptionsProvider<ISpotifyUserClientOptions>>();
                    client.BaseAddress = optionsProvider.Get().UserInformationEndpoint;
                })
                .AddHttpMessageHandler<SpotifyRegularErrorHandler<ISpotifyUserClient>>();

            services.TryAddSingleton<ISpotifyUserClient, SpotifyUserClient>();

            return httpClientBuilder;
        }
    }
}
