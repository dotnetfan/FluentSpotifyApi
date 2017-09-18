using System;
using Microsoft.Extensions.DependencyInjection;

namespace FluentSpotifyApi.Core.Client
{
    /// <summary>
    /// The pipeline item.
    /// </summary>
    public interface IPipelineItem
    {
        /// <summary>
        /// Configures the specified services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="currentHttpClientWrapperFactory">The current HTTP client wrapper factory.</param>
        /// <returns></returns>
        Func<IServiceProvider, IHttpClientWrapper, IHttpClientWrapper> Configure(IServiceCollection services, Func<IServiceProvider, IHttpClientWrapper> currentHttpClientWrapperFactory);
    }
}
