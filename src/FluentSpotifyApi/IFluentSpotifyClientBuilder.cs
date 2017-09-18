using System;
using System.Net.Http;
using FluentSpotifyApi.Core.Client;
using FluentSpotifyApi.Options;

namespace FluentSpotifyApi
{
    /// <summary>
    /// The <see cref="IFluentSpotifyClient"/> builder.
    /// </summary>
    public interface IFluentSpotifyClientBuilder
    {
        /// <summary>
        /// Configures options.
        /// </summary>
        /// <param name="configureOptions">The options configuration action.</param>
        /// <returns></returns>
        IFluentSpotifyClientBuilder ConfigureOptions(Action<FluentSpotifyClientOptions> configureOptions);

        /// <summary>
        /// Configures pipeline.
        /// </summary>
        /// <param name="configurePipeline">The pipeline configuration action.</param>
        /// <returns></returns>
        IFluentSpotifyClientBuilder ConfigurePipeline(Action<IPipeline> configurePipeline);

        /// <summary>
        /// Configures HTTP client. 
        /// </summary>
        /// <param name="configureHttpClient">The HTTP client configuration action.</param>
        /// <returns></returns>
        IFluentSpotifyClientBuilder ConfigureHttpClient(Action<HttpClient> configureHttpClient);

        /// <summary>
        /// Uses HTTP client factory. If not called a default HTTP client with timeout set to 1 minute is created.
        /// </summary>
        /// <param name="httpClientFactory">The HTTP client factory.</param>
        /// <param name="disposeWithServiceProvider">
        /// If <c>true</c> the HTTP Client obtained from the <paramref name="httpClientFactory"/> will be disposed together
        /// with <see cref="IServiceProvider"/>. Set to <c>false</c> by default.
        /// </param>
        /// <returns></returns>
        IFluentSpotifyClientBuilder UseHttpClientFactory(Func<IServiceProvider, HttpClient> httpClientFactory, bool disposeWithServiceProvider = false);
    }
}
