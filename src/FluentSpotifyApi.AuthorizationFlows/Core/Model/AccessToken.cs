using System;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Model
{
    /// <summary>
    /// The access token.
    /// </summary>
    public class AccessToken
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccessToken"/> class.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="expiresAt">The expires at.</param>
        public AccessToken(string token, DateTimeOffset expiresAt)
        {
            this.Token = token;
            this.ExpiresAt = expiresAt;
        }

        /// <summary>
        /// Gets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        public string Token { get; private set; }

        /// <summary>
        /// Gets the expires at.
        /// </summary>
        /// <value>
        /// The expires at.
        /// </value>
        public DateTimeOffset ExpiresAt { get; private set; }
    }
}
