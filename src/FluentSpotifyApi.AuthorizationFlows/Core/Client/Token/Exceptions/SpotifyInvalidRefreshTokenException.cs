using System.Net;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.Token.Exceptions
{
    /// <summary>
    /// The exception that is thrown when provided refresh token is invalid.
    /// This typically occurs when user revokes access to the Spotify application.
    /// </summary>
    /// <seealso cref="SpotifyAuthenticationErrorException" />
    public class SpotifyInvalidRefreshTokenException : SpotifyAuthenticationErrorException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyInvalidRefreshTokenException" /> class.
        /// </summary>
        /// <param name="errorCode">The error status code.</param>
        /// <param name="content">The content.</param>
        /// <param name="error">The error.</param>
        public SpotifyInvalidRefreshTokenException(HttpStatusCode errorCode, string content, AuthenticationError error)
            : base(errorCode, content, error)
        {
        }
    }
}
