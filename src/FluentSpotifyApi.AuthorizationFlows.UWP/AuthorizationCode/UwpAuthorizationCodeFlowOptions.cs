using System;
using System.Collections.Generic;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.Authorization;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.User;
using FluentSpotifyApi.Core.Options;

namespace FluentSpotifyApi.AuthorizationFlows.UWP.AuthorizationCode
{
    /// <summary>
    /// The UWP authorization code flow options.
    /// </summary>
    public sealed class UwpAuthorizationCodeFlowOptions : IAuthorizationOptions, IUserClientOptions, IValidatable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UwpAuthorizationCodeFlowOptions"/> class.
        /// </summary>
        public UwpAuthorizationCodeFlowOptions()
        {
            this.AuthorizationEndpoint = AuthorizationFlows.Core.Client.Authorization.Defaults.AuthorizationEndpoint;
            this.UserInformationEndpoint = AuthorizationFlows.Core.Client.User.Defaults.UserInformationEndpoint;
        }

        /// <summary>
        /// Gets the Client ID.
        /// </summary>
        /// <value>
        /// The client identifier.
        /// </value>
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the URL for getting authorization from the Spotify Accounts Service. Set to https://accounts.spotify.com/authorize by default.
        /// </summary>
        /// <value>
        /// The URL for getting authorization from the Spotify Accounts Service.
        /// </value>
        public Uri AuthorizationEndpoint { get; set; }

        /// <summary>
        /// Gets or sets the URL for getting info about current user. Set to https://api.spotify.com/v1/me by default.
        /// </summary>
        /// <value>
        /// The URL for getting info about current user.
        /// </value>
        public Uri UserInformationEndpoint { get; set; }

        /// <summary>
        /// Gets or sets the list of strongly typed permissions. Can be used together with <see cref="DynamicScopes"/>.
        /// </summary>
        /// <value>
        /// The list of strongly typed permissions.
        /// </value>
        public IList<Scope> Scopes { get; set; }

        /// <summary>
        /// Gets or sets the list of permission strings. Can be used together with <see cref="Scopes"/>.
        /// </summary>
        /// <value>
        /// The list of permission strings.
        /// </value>
        public IList<string> DynamicScopes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether user is forced to re-approve the app during authentication. Set to <c>false</c> by default.
        /// </summary>
        /// <value>
        /// If set to <c>true</c> the user is forced to re-approve the app during authentication.
        /// </value>
        public bool ShowDialog { get; set; }

        /// <summary>
        /// Performs validation.
        /// </summary>
        public void Validate()
        {
            if (string.IsNullOrEmpty(this.ClientId))
            {
                throw new ArgumentException($"{nameof(this.ClientId)} must be a non-empty string.", nameof(this.ClientId));
            }

            if (this.AuthorizationEndpoint == null)
            {
                throw new ArgumentNullException(nameof(this.AuthorizationEndpoint));
            }

            if (this.UserInformationEndpoint == null)
            {
                throw new ArgumentNullException(nameof(this.UserInformationEndpoint));
            }
        }
    }
}
