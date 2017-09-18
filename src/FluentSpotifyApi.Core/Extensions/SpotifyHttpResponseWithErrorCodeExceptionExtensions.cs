using System.Collections.Generic;
using System.Net;
using FluentSpotifyApi.Core.Exceptions;

namespace FluentSpotifyApi.Core.Extensions
{
    /// <summary>
    /// The set of <see cref="SpotifyHttpResponseWithErrorCodeException"/> exceptions.
    /// </summary>
    public static class SpotifyHttpResponseWithErrorCodeExceptionExtensions
    {
        private static readonly IList<HttpStatusCode> RecoverableErrorCodes = new[] 
        {
            HttpStatusCode.RequestTimeout,
            HttpStatusCode.InternalServerError,
            HttpStatusCode.BadGateway,
            HttpStatusCode.ServiceUnavailable,
            HttpStatusCode.GatewayTimeout
        };

        /// <summary>
        /// Determines whether the <see cref="SpotifyHttpResponseWithErrorCodeException.ErrorCode"/> is one of the following:
        /// <see cref="HttpStatusCode.RequestTimeout"/>
        /// <see cref="HttpStatusCode.InternalServerError"/>
        /// <see cref="HttpStatusCode.BadGateway"/>
        /// <see cref="HttpStatusCode.ServiceUnavailable"/>
        /// <see cref="HttpStatusCode.GatewayTimeout"/>
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns>
        ///   <c>true</c> if the specified exception is recoverable; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsRecoverable(this SpotifyHttpResponseWithErrorCodeException exception)
        {
            return RecoverableErrorCodes.Contains(exception.ErrorCode);
        }
    }
}
