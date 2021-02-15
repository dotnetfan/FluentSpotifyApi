using System;
using System.Net.Http;

namespace FluentSpotifyApi.Core.Exceptions
{
    /// <summary>
    /// The exception that shields <see cref="HttpRequestException"/> thrown by <see cref="HttpClient"/>.
    /// The original <see cref="HttpRequestException"/> can be found in <see cref="Exception.InnerException"/>.
    /// </summary>
    /// <seealso cref="SpotifyCommunicationException" />
    public class SpotifyHttpRequestException : SpotifyCommunicationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyHttpRequestException"/> class.
        /// </summary>
        /// <param name="clientType">The client type.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.
        /// </param>
        public SpotifyHttpRequestException(Type clientType, HttpRequestException innerException)
            : base("An error has occurred while sending request to the server. See inner exception for details.", clientType, innerException)
        {
        }
    }
}
