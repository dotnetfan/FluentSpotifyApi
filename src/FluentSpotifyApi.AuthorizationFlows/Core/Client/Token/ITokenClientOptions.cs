using System;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.Token
{
    /// <summary>
    /// The token client options.
    /// </summary>
    public interface ITokenClientOptions
    {
        /// <summary>
        /// Gets the Client ID.
        /// </summary>
        /// <value>
        /// The Client ID.
        /// </value>
        string ClientId { get; }

        /// <summary>
        /// Gets the Client Secret.
        /// </summary>
        /// <value>
        /// The client secret.
        /// </value>
        string ClientSecret { get; }

        /// <summary>
        /// Gets the URL for getting OAuth tokens from Spotify Accounts Service.
        /// </summary>
        /// <value>
        /// The URL for getting OAuth tokens from Spotify Accounts Service.
        /// </value>
        Uri TokenEndpoint { get; }
    }
}
