using System;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.Token
{
    /// <summary>
    /// The options interface for <see cref="ISpotifyTokenClient"/>.
    /// </summary>
    public interface ISpotifyTokenClientOptions
    {
        /// <summary>
        /// Gets the Client ID.
        /// </summary>
        string ClientId { get; }

        /// <summary>
        /// Gets the Client Secret.
        /// </summary>
        string ClientSecret { get; }

        /// <summary>
        /// Gets the URI of Spotify Accounts Service endpoint for obtaining OAuth tokens.
        /// </summary>
        Uri TokenEndpoint { get; }
    }
}
