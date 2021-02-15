using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.Authorization.DependencyInjection
{
    /// <summary>
    /// The extension methods for registering <see cref="ISpotifyAuthorizationClient"/> implementation in an <see cref="IServiceCollection"/>.
    /// </summary>
    public static class SpotifyAuthorizationClientServiceCollectionExtensions
    {
        /// <summary>
        /// Adds <see cref="ISpotifyAuthorizationClient"/> implementation to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/>.</param>
        public static IServiceCollection AddSpotifyAuthorizationClient(this IServiceCollection services)
        {
            services.TryAddSingleton<ICsrfTokenProvider, CsrfTokenProvider>();
            services.TryAddSingleton<ICodeVerifierProvider, CodeVerifierProvider>();
            services.TryAddSingleton<ISpotifyAuthorizationClient, SpotifyAuthorizationClient>();

            return services;
        }
    }
}
