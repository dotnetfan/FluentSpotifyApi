using System;
using System.Net;

namespace FluentSpotifyApi.Core.Exceptions
{
    /// <summary>
    /// The exception that is thrown when a response with error <see cref="HttpStatusCode"/> and Spotify error content of type <typeparamref name="TError"/>
    /// is returned from the Spotify service.
    /// </summary>
    /// <typeparam name="TError">The type of error.</typeparam>
    /// <seealso cref="FluentSpotifyApi.Core.Exceptions.SpotifyHttpResponseException" />
    public class SpotifyHttpResponseException<TError> : SpotifyHttpResponseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyHttpResponseException{TPayload}" /> class.
        /// </summary>
        /// <param name="clientType">The client type.</param>
        /// <param name="errorCode">The error status code.</param>
        /// <param name="content">The content.</param>
        /// <param name="error">The error.</param>
        public SpotifyHttpResponseException(Type clientType, HttpStatusCode errorCode, string content, TError error)
            : base(clientType, errorCode, content)
        {
            this.Error = error;
        }

        /// <summary>
        /// Gets the error.
        /// </summary>
        public TError Error { get; }
    }
}
