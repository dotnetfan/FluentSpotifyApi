using System;
using Microsoft.Extensions.DependencyInjection;

namespace FluentSpotifyApi.AuthorizationFlows.ClientCredentials.DependencyInjection
{
    /// <summary>
    /// A builder that can be used to configure the Spotify Client Credentials Flow.
    /// </summary>
    public interface ISpotifyClientCredentialsFlowBuilder
    {
        /// <summary>
        /// Configures HTTP client builder for <see cref="Core.Client.Token.ISpotifyTokenClient"/> HTTP client.
        /// </summary>
        /// <param name="configureBuilder">Used to configure the <see cref="IHttpClientBuilder"/>.</param>
        ISpotifyClientCredentialsFlowBuilder ConfigureTokenHttpClientBuilder(Action<IHttpClientBuilder> configureBuilder);
    }
}
