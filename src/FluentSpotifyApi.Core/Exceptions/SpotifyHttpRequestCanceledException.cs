using System;
using System.Threading;

namespace FluentSpotifyApi.Core.Exceptions
{
    /// <summary>
    /// The exception that is thrown when the <see cref="OperationCanceledException"/> is thrown by 
    /// <see cref="System.Net.Http.HttpClient.SendAsync(System.Net.Http.HttpRequestMessage, System.Threading.CancellationToken)"/> 
    /// or during processing of the response stream when specified <see cref="CancellationToken.IsCancellationRequested"/> is <c>false</c>. 
    /// This usually means that <see cref="System.Net.Http.HttpClient.Timeout"/> has exceeded.
    /// </summary>
    /// <seealso cref="FluentSpotifyApi.Core.Exceptions.SpotifyHttpRequestException" />
    public class SpotifyHttpRequestCanceledException : SpotifyHttpRequestException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyHttpRequestCanceledException" /> class.
        /// </summary>
        /// <param name="operationCanceledException">The operation canceled exception.</param>
        public SpotifyHttpRequestCanceledException(OperationCanceledException operationCanceledException) 
            : base("The HTTP request has been canceled from an internal cancellation token.", operationCanceledException)
        {
        }
    }
}
