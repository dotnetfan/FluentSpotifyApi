namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.Token
{
    /// <summary>
    /// The access token response interface.
    /// </summary>
    public interface IAccessTokenResponse
    {
        /// <summary>
        /// An access token that can be provided in subsequent calls, for example to Spotify Web API services.
        /// </summary>
        string AccessToken { get; }

        /// <summary>
        /// How the access token may be used: always “Bearer”.
        /// </summary>
        string TokenType { get; set; }

        /// <summary>
        /// The time period (in seconds) for which the access token is valid.
        /// </summary>
        int ExpiresIn { get; }
    }
}
