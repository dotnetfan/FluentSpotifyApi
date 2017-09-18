using System;
using FluentSpotifyApi.AuthorizationFlows.Core.Client;
using FluentSpotifyApi.Core.Client;
using FluentSpotifyApi.Core.Internal.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Pipeline
{
    /// <summary>
    /// The base class for authorization pipeline item.
    /// </summary>
    public abstract class AuthorizationPipelineItemBase : IPipelineItem
    {
        /// <summary>
        /// Configures the specified services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="currentHttpClientWrapperFactory">The current HTTP client wrapper factory.</param>
        /// <returns></returns>
        public Func<IServiceProvider, IHttpClientWrapper, IHttpClientWrapper> Configure(IServiceCollection services, Func<IServiceProvider, IHttpClientWrapper> currentHttpClientWrapperFactory)
        {
            services.RegisterSingleton<IAuthorizationFlowsHttpClient>(serviceProvider => new AuthorizationFlowsHttpClient(currentHttpClientWrapperFactory(serviceProvider)));

            this.Configure(services);

            return (serviceProvider, httpClientWrapper) =>
                            new AuthenticatedHttpClient(
                                httpClientWrapper,
                                serviceProvider.GetRequiredService<IAuthenticationTicketProvider>());
        }

        /// <summary>
        /// Configures the specified services.
        /// </summary>
        /// <param name="services">The services.</param>
        protected abstract void Configure(IServiceCollection services);
    }
}
