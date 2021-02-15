using System;
using System.Net.Http;

namespace FluentSpotifyApi.Core.Exceptions
{
    /// <summary>
    /// The exception that shields <see cref="OperationCanceledException"/> caused by <see cref="HttpClient"/> internal cancellation token representing timeout.
    /// The original <see cref="OperationCanceledException"/> can be found in <see cref="Exception.InnerException"/>.
    /// </summary>
    /// <seealso cref="FluentSpotifyApi.Core.Exceptions.SpotifyHttpRequestException" />
    public class SpotifyHttpClientTimeoutException : SpotifyCommunicationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyHttpClientTimeoutException" /> class.
        /// </summary>
        /// <param name="clientType">The client type.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.
        /// </param>
        public SpotifyHttpClientTimeoutException(Type clientType, OperationCanceledException innerException)
            : base("The operation has been canceled due to the HttpClient timeout. See inner exception for details.", clientType, innerException)
        {
        }
    }
}
