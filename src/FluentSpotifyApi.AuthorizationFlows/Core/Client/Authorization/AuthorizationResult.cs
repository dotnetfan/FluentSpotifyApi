using System;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.Authorization
{
    /// <summary>
    /// The authorization result.
    /// </summary>
    public class AuthorizationResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationResult"/> class.
        /// </summary>
        /// <param name="authorizationCode">The authorization code returned from Spotify Account Service that can be exchanged for access token.</param>
        /// <param name="redirectUri">The redirect URI provided by <see cref="IAuthorizationRedirectUriProvider"/>.</param>
        public AuthorizationResult(string authorizationCode, Uri redirectUri)
        {
            this.AuthorizationCode = authorizationCode;
            this.RedirectUri = redirectUri;
        }

        /// <summary>
        /// Gets the authorization code returned from Spotify Account Service that can be exchanged for access token.
        /// </summary>
        public string AuthorizationCode { get; }

        /// <summary>
        /// Gets the redirect URI provided by <see cref="IAuthorizationRedirectUriProvider"/>.
        /// </summary>
        public Uri RedirectUri { get; }
    }
}
