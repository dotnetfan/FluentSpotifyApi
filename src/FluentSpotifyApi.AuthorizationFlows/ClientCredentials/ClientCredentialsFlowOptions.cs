using System;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.Token;
using FluentSpotifyApi.Core.Options;

namespace FluentSpotifyApi.AuthorizationFlows.ClientCredentials
{
    /// <summary>
    /// The client credentials flow options.
    /// </summary>
    public sealed class ClientCredentialsFlowOptions : ITokenClientOptions, IValidatable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientCredentialsFlowOptions"/> class.
        /// </summary>
        public ClientCredentialsFlowOptions()
        {
            this.TokenEndpoint = Defaults.TokenEndpoint;
        }

        /// <summary>
        /// Gets or sets the Client ID.
        /// </summary>
        /// <value>
        /// The Client ID.
        /// </value>
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the Client Secret.
        /// </summary>
        /// <value>
        /// The Client Secret.
        /// </value>
        public string ClientSecret { get; set; }

        /// <summary>
        /// Gets or sets the URL for getting OAuth tokens from Spotify Accounts Service. Set to https://accounts.spotify.com/api/token by default.
        /// </summary>
        /// <value>
        /// The URL for getting OAuth tokens.
        /// </value>
        public Uri TokenEndpoint { get; set; }

        /// <summary>
        /// Performs validation.
        /// </summary>
        public void Validate()
        {
            if (string.IsNullOrEmpty(this.ClientId))
            {
                throw new ArgumentException($"{nameof(this.ClientId)} must be a non-empty string.", nameof(this.ClientId));
            }

            if (string.IsNullOrEmpty(this.ClientSecret))
            {
                throw new ArgumentException($"{nameof(this.ClientSecret)} must be a non-empty string.", nameof(this.ClientSecret));
            }

            if (this.TokenEndpoint == null)
            {
                throw new ArgumentNullException(nameof(this.TokenEndpoint));
            }
        }
    }
}
