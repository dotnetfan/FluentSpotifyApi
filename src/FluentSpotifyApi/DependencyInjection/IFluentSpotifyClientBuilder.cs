using System;
using Microsoft.Extensions.DependencyInjection;

namespace FluentSpotifyApi.DependencyInjection
{
    /// <summary>
    /// A builder that can be used to configure <see cref="IFluentSpotifyClient"/>.
    /// </summary>
    public interface IFluentSpotifyClientBuilder
    {
        /// <summary>
        /// Configures HTTP client builder for <see cref="IFluentSpotifyClient"/> HTTP client.
        /// </summary>
        /// <param name="configureBuilder">Used to configure the <see cref="IHttpClientBuilder"/>.</param>
        /// <returns></returns>
        IFluentSpotifyClientBuilder ConfigureHttpClientBuilder(Action<IHttpClientBuilder> configureBuilder);
    }
}
