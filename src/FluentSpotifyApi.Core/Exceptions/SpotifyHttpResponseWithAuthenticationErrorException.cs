using System.Net;
using System.Net.Http.Headers;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.Core.Exceptions
{
    /// <summary>
    /// The exception that is thrown when a response with error <see cref="HttpStatusCode"/> and payload of type <see cref="AuthenticationError"/> is returned from the Spotify service. 
    /// </summary>
    public class SpotifyHttpResponseWithAuthenticationErrorException : SpotifyHttpResponseWithPayloadException<AuthenticationError>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyHttpResponseWithAuthenticationErrorException" /> class.
        /// </summary>
        /// <param name="errorCode">The error status code.</param>
        /// <param name="httpResponseHeaders">The reponse headers</param>
        /// <param name="content">The content.</param>
        /// <param name="payload">The payload.</param>
        public SpotifyHttpResponseWithAuthenticationErrorException(HttpStatusCode errorCode, HttpResponseHeaders httpResponseHeaders, string content, AuthenticationError payload) : base(errorCode, httpResponseHeaders, content, payload)
        {
        }
    }
}
