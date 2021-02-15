using System.Net;
using FluentSpotifyApi.Core.Exceptions;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.Token.Exceptions
{
    /// <summary>
    /// The exception that is thrown when a response with error <see cref="HttpStatusCode"/> and Spotify error content of type <see cref="AuthenticationError"/>
    /// is returned from the Spotify service.
    /// </summary>
    public class SpotifyAuthenticationErrorException : SpotifyHttpResponseException<AuthenticationError>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyAuthenticationErrorException" /> class.
        /// </summary>
        /// <param name="errorCode">The error status code.</param>
        /// <param name="content">The content.</param>
        /// <param name="error">The error.</param>
        public SpotifyAuthenticationErrorException(HttpStatusCode errorCode, string content, AuthenticationError error)
            : base(typeof(ISpotifyTokenClient), errorCode, content, error)
        {
        }
    }
}
