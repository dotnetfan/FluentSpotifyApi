using System;
using FluentSpotifyApi.Core.Client;
using FluentSpotifyApi.Extensions;
using FluentSpotifyApi.Options;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace FluentSpotifyApi.UnitTesting.Extensions
{
    /// <summary>
    /// The extension methods for registering <see cref="IFluentSpotifyClient"/> in <see cref="IServiceCollection"/> for unit testing purposes.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the fluent spotify client for unit testing.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="httpClientWrapperMock">The HTTP client wrapper mock.</param>
        /// <param name="configurePipeline">The configure pipeline.</param>
        /// <param name="configureOptions">The configure options action.</param>
        /// <returns></returns>
        public static IServiceCollection AddFluentSpotifyClientForUnitTesting(
            this IServiceCollection services, 
            Mock<IHttpClientWrapper> httpClientWrapperMock = null, 
            Action<IPipeline> configurePipeline = null, 
            Action<FluentSpotifyClientOptions> configureOptions = null)
        {
            Mock<IPipelineItem> pipelineItemMock = null;

            if (httpClientWrapperMock != null)
            {
                pipelineItemMock = new Mock<IPipelineItem>();
                pipelineItemMock.Setup(x => x.Configure(
                    It.IsAny<IServiceCollection>(),
                    It.IsAny<Func<IServiceProvider, IHttpClientWrapper>>())).Returns((provider, httpClientWrapper) => httpClientWrapperMock.Object);
            }

            return services
                .AddFluentSpotifyClient(clientBuilder => clientBuilder
                    .ConfigureOptions(configureOptions)
                    .ConfigurePipeline(pipeline =>
                    {
                        configurePipeline?.Invoke(pipeline);

                        if (pipelineItemMock != null)
                        {
                            pipeline.Add(pipelineItemMock.Object);
                        }
                    })
                    .UseHttpClientFactory(serviceProvider => null)); 
        }
    }
}
