using System.Net;
using System.Net.Http.Headers;

namespace FluentSpotifyApi.Core.Exceptions
{
    /// <summary>
    /// The exception that is thrown when a response with error <see cref="HttpStatusCode"/> and payload of type <typeparamref name="TPayload"/> is returned from the Spotify service. 
    /// </summary>
    /// <typeparam name="TPayload">The type of the payload.</typeparam>
    /// <seealso cref="FluentSpotifyApi.Core.Exceptions.SpotifyHttpResponseWithErrorCodeException" />
    public class SpotifyHttpResponseWithPayloadException<TPayload> : SpotifyHttpResponseWithErrorCodeException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyHttpResponseWithPayloadException{TPayload}" /> class.
        /// </summary>
        /// <param name="errorCode">The error status code.</param>
        /// <param name="httpResponseHeaders">The HTTP response headers.</param>
        /// <param name="content">The content.</param>
        /// <param name="payload">The payload.</param>
        public SpotifyHttpResponseWithPayloadException(HttpStatusCode errorCode, HttpResponseHeaders httpResponseHeaders, string content, TPayload payload) : base(errorCode, httpResponseHeaders, content)
        {
            this.Payload = payload;
        }

        /// <summary>
        /// Gets the payload.
        /// </summary>
        /// <value>
        /// The payload.
        /// </value>
        public TPayload Payload { get; }
    }
}
