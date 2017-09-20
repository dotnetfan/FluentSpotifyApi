using System.Net;
using System.Net.Http.Headers;

namespace FluentSpotifyApi.Core.Exceptions
{
    /// <summary>
    /// The exception that is thrown when a response with error <see cref="HttpStatusCode"/> is returned from the Spotify service. 
    /// </summary>
    public class SpotifyHttpResponseWithErrorCodeException : SpotifyServiceException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyHttpResponseWithErrorCodeException" /> class.
        /// </summary>
        /// <param name="errorCode">The error status code.</param>
        /// <param name="httpResponseHeaders">The HTTP response headers.</param>
        /// <param name="content">The content.</param>
        public SpotifyHttpResponseWithErrorCodeException(HttpStatusCode errorCode, HttpResponseHeaders httpResponseHeaders, string content) : base(content)
        {
            this.ErrorCode = errorCode;
            this.Headers = new SpotifyHttpResponseHeaders(httpResponseHeaders);
        }

        /// <summary>
        /// Gets the status code.
        /// </summary>
        /// <value>
        /// The status code.
        /// </value>
        public HttpStatusCode ErrorCode { get; }

        /// <summary>
        /// Gets the headers.
        /// </summary>
        /// <value>
        /// The headers.
        /// </value>
        public SpotifyHttpResponseHeaders Headers { get; }
    }
}
