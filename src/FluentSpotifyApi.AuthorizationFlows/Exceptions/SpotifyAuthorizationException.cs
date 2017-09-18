using System;
using FluentSpotifyApi.Core.Exceptions;

namespace FluentSpotifyApi.AuthorizationFlows.Exceptions
{
    /// <summary>
    /// The exception that is thrown when user authorization fails.
    /// </summary>
    /// <seealso cref="FluentSpotifyApi.Core.Exceptions.SpotifyServiceException" />
    public class SpotifyAuthorizationException : SpotifyServiceException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyAuthorizationException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public SpotifyAuthorizationException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyAuthorizationException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.
        /// </param>
        public SpotifyAuthorizationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
