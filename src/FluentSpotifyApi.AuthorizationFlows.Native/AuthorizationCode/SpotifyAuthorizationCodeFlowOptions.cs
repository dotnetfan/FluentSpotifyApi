using System;
using System.Collections.Generic;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.Authorization;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.Token;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.User;
using FluentSpotifyApi.Core.Utils;

namespace FluentSpotifyApi.AuthorizationFlows.Native.AuthorizationCode
{
    /// <summary>
    /// The Spotify Authorization Code Flow Options for native apps.
    /// </summary>
    public class SpotifyAuthorizationCodeFlowOptions : ISpotifyAuthorizationClientOptions, ISpotifyUserClientOptions, ISpotifyTokenClientOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyAuthorizationCodeFlowOptions"/> class.
        /// </summary>
        public SpotifyAuthorizationCodeFlowOptions()
        {
            this.UserClaimResolvers.AddOrUpdate(UserClaimTypes.Id, u => u.Id);
            this.UserClaimResolvers.AddOrUpdate(UserClaimTypes.DisplayName, u => !string.IsNullOrEmpty(u.DisplayName) ? u.DisplayName : u.Id);
        }

        /// <summary>
        /// Gets the Client ID.
        /// </summary>
        public string ClientId { get; set; }

        string ISpotifyTokenClientOptions.ClientSecret => null;

        /// <summary>
        /// Gets or sets the URI of Spotify Accounts Service endpoint for performing user authorization. Defaults to https://accounts.spotify.com/authorize.
        /// </summary>
        public Uri AuthorizationEndpoint { get; set; } = SpotifyAuthorizationClientDefaults.AuthorizationEndpoint;

        /// <summary>
        /// Gets or sets the URI of Spotify Web API endpoint for obtaining user information. Defaults to https://api.spotify.com/v1/me.
        /// </summary>
        public Uri UserInformationEndpoint { get; set; } = SpotifyUserClientDefaults.UserInformationEndpoint;

        /// <summary>
        /// Gets or sets the URI of Spotify Accounts Service endpoint for obtaining OAuth tokens. Defaults to https://accounts.spotify.com/api/token.
        /// </summary>
        public Uri TokenEndpoint { get; set; } = SpotifyTokenClientDefaults.TokenEndpoint;

        /// <summary>
        /// Gets the collection of user claim resolvers used to resolve user claims from <see cref="FluentSpotifyApi.Core.Model.PrivateUser"/>.
        /// The collection is initialized with <see cref="UserClaimTypes.Id"/> and <see cref="UserClaimTypes.DisplayName"/> claims.
        /// </summary>
        public UserClaimResolvers UserClaimResolvers { get; } = new UserClaimResolvers();

        /// <summary>
        /// Gets the list of permissions requested by app. Use <see cref="SpotifyScopes"/> for well-known permissions.
        /// </summary>
        public ICollection<string> Scopes { get; } = new HashSet<string>();

        /// <summary>
        /// Gets or sets a value indicating whether user is forced to re-approve the app during user authorization. Defaults to <c>false</c>.
        /// </summary>
        public bool ShowDialog { get; set; }

        /// <summary>
        /// Performs validation.
        /// </summary>
        public virtual void Validate()
        {
            SpotifyArgumentAssertUtils.ThrowIfNullOrEmpty(this.ClientId, nameof(this.ClientId));
            SpotifyArgumentAssertUtils.ThrowIfNull(this.AuthorizationEndpoint, nameof(this.AuthorizationEndpoint));
            SpotifyArgumentAssertUtils.ThrowIfNull(this.UserInformationEndpoint, nameof(this.UserInformationEndpoint));
            SpotifyArgumentAssertUtils.ThrowIfNull(this.TokenEndpoint, nameof(this.TokenEndpoint));
        }
    }
}
