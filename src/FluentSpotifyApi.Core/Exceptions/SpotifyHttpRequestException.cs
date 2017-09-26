using System;

namespace FluentSpotifyApi.Core.Exceptions
{
    /// <summary>
    /// The exception that shields any exception thrown by <see cref="System.Net.Http.HttpClient.SendAsync(System.Net.Http.HttpRequestMessage, System.Threading.CancellationToken)"/> 
    /// or during processing of the response stream. The inner exception always contains the original exception.
    /// </summary>
    /// <seealso cref="SpotifyServiceException" />
    public class SpotifyHttpRequestException : SpotifyServiceException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyHttpRequestException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public SpotifyHttpRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
