using System.Net;
using System.Net.Http.Headers;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.Core.Exceptions
{
    /// <summary>
    /// The exception that is thrown when a response with error <see cref="HttpStatusCode"/> and payload of type <see cref="RegularError"/> is returned from the Spotify service. 
    /// </summary>
    public class SpotifyHttpResponseWithRegularErrorException : SpotifyHttpResponseWithPayloadException<RegularError>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyHttpResponseWithRegularErrorException" /> class.
        /// </summary>
        /// <param name="errorCode">The error status code.</param>
        /// <param name="httpResponseHeaders">The HTTP response headers.</param>
        /// <param name="content">The content.</param>
        /// <param name="payload">The payload.</param>
        public SpotifyHttpResponseWithRegularErrorException(HttpStatusCode errorCode, HttpResponseHeaders httpResponseHeaders, string content, RegularError payload) : base(errorCode, httpResponseHeaders, content, payload)
        {
        }
    }
}
