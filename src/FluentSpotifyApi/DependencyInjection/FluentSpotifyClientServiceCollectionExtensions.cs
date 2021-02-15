using System;
using FluentSpotifyApi.Core.HttpHandlers;
using FluentSpotifyApi.Core.Options;
using FluentSpotifyApi.Core.User;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FluentSpotifyApi.DependencyInjection
{
    /// <summary>
    /// The extension methods for registering <see cref="IFluentSpotifyClient"/> implementation in an <see cref="IServiceCollection"/>.
    /// </summary>
    public static class FluentSpotifyClientServiceCollectionExtensions
    {
        /// <summary>
        /// Adds <see cref="IFluentSpotifyClient"/> implementation to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/>.</param>
        public static IFluentSpotifyClientBuilder AddFluentSpotifyClient(this IServiceCollection services)
        {
            return services.AddFluentSpotifyClient(o => { });
        }

        /// <summary>
        /// Adds <see cref="IFluentSpotifyClient"/> implementation to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/>.</param>
        /// <param name="configureOptions">Used to configure the <see cref="FluentSpotifyClientOptions"/>.</param>
        public static IFluentSpotifyClientBuilder AddFluentSpotifyClient(this IServiceCollection services, Action<FluentSpotifyClientOptions> configureOptions)
        {
            services
                .AddOptions<FluentSpotifyClientOptions>().Validate(o =>
                {
                    o.Validate();
                    return true;
                })
                .Configure(configureOptions);

            services.TryAddSingleton<IOptionsProvider<FluentSpotifyClientOptions>, OptionsProvider<FluentSpotifyClientOptions>>();

            services.TryAddSingleton<IFluentSpotifyHttpClientFactory, FluentSpotifyHttpClientFactory>();
            services.TryAddTransient<SpotifyRegularErrorHandler<IFluentSpotifyClient>>();
            var httpClientBuilder = services.AddHttpClient(FluentSpotifyHttpClientFactory.ClientName)
                .ConfigureHttpClient((sp, client) =>
                {
                    var optionsProvider = sp.GetRequiredService<IOptionsProvider<FluentSpotifyClientOptions>>();
                    client.BaseAddress = optionsProvider.Get().WebApiEndpoint;
                })
                .AddHttpMessageHandler<SpotifyRegularErrorHandler<IFluentSpotifyClient>>();

            services.TryAddSingleton<IFluentSpotifyClient>(sp =>
            {
                var currentUserProvider = sp.GetService<ICurrentUserProvider>() ?? new NotSupportedCurrentUserProvider();
                return ActivatorUtilities.CreateInstance<FluentSpotifyClient>(sp, currentUserProvider);
            });

            return new FluentSpotifyClientBuilder(httpClientBuilder);
        }
    }
}
