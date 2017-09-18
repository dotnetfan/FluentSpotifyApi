using System;
using Microsoft.Extensions.DependencyInjection;

namespace FluentSpotifyApi.Extensions
{
    /// <summary>
    /// The extension methods for registering <see cref="IFluentSpotifyClient"/> in <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers <see cref="IFluentSpotifyClient" /> in <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The service collection.</param>
        public static IServiceCollection AddFluentSpotifyClient(this IServiceCollection services)
        {
            return services.AddFluentSpotifyClient(null);
        }

        /// <summary>
        /// Registers <see cref="IFluentSpotifyClient" /> in <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="buildClient">The client builder.</param>
        public static IServiceCollection AddFluentSpotifyClient(this IServiceCollection services, Action<IFluentSpotifyClientBuilder> buildClient)
        {
            var clientBuilder = new FluentSpotifyClientBuilder();
            buildClient?.Invoke(clientBuilder);
            clientBuilder.Configure(services);

            return services;
        }
    }
}
