namespace FluentSpotifyApi.AuthorizationFlows.AuthorizationCode.Native
{
    /// <summary>
    /// The authorization tokens returned from the token proxy service.
    /// </summary>
    public class ProxyAuthorizationTokens
    {
        /// <summary>
        /// Gets or sets the authorization key that is used for getting access tokens from the token service.
        /// </summary>
        /// <value>
        /// The authorization key.
        /// </value>
        public string AuthorizationKey { get; set; }

        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        /// <value>
        /// The access token.
        /// </value>
        public ProxyAccessToken AccessToken { get; set; }
    }
}
