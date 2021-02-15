using System;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.Token;
using FluentSpotifyApi.Core.Utils;

namespace FluentSpotifyApi.AuthorizationFlows.ClientCredentials
{
    /// <summary>
    /// The Spotify Client Credentials Flow Options.
    /// </summary>
    public sealed class SpotifyClientCredentialsFlowOptions : ISpotifyTokenClientOptions
    {
        /// <summary>
        /// Gets or sets the Client ID.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the Client Secret.
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// Gets or sets the URI of Spotify Accounts Service endpoint for obtaining OAuth tokens. Defaults to https://accounts.spotify.com/api/token.
        /// </summary>
        public Uri TokenEndpoint { get; set; } = SpotifyTokenClientDefaults.TokenEndpoint;

        /// <summary>
        /// Performs validation.
        /// </summary>
        public void Validate()
        {
            SpotifyArgumentAssertUtils.ThrowIfNullOrEmpty(this.ClientId, nameof(this.ClientId));
            SpotifyArgumentAssertUtils.ThrowIfNullOrEmpty(this.ClientSecret, nameof(this.ClientSecret));
            SpotifyArgumentAssertUtils.ThrowIfNull(this.TokenEndpoint, nameof(this.TokenEndpoint));
        }
    }
}
