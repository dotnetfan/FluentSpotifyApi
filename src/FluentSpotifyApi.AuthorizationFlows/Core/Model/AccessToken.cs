using System;
using FluentSpotifyApi.AuthorizationFlows.Core.Time;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Model
{
    /// <summary>
    /// The access token.
    /// </summary>
    public class AccessToken
    {
        private static readonly TimeSpan ExpirationMargin = TimeSpan.FromSeconds(5);

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
        public string Token { get; }

        /// <summary>
        /// Gets the expires at.
        /// </summary>
        public DateTimeOffset ExpiresAt { get; }

        /// <summary>
        /// Checks whether the access token is expired.
        /// </summary>
        /// <param name="clock">The clock instance.</param>
        /// <returns>
        /// </returns>
        public bool IsExpired(IClock clock)
        {
            return this.ExpiresAt - clock.GetUtcNow() < ExpirationMargin;
        }
    }
}
