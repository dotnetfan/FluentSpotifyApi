using System;
using FluentSpotifyApi.Core.Exceptions;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Client.Authorization.Exceptions
{
    /// <summary>
    /// The exception that is thrown when user authorization fails.
    /// </summary>
    /// <seealso cref="SpotifyCommunicationException" />
    public class SpotifyAuthorizationException : SpotifyCommunicationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyAuthorizationException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public SpotifyAuthorizationException(string message)
            : base(message, typeof(ISpotifyAuthorizationClient))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyAuthorizationException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.
        /// </param>
        public SpotifyAuthorizationException(string message, Exception innerException)
            : base(message, typeof(ISpotifyAuthorizationClient), innerException)
        {
        }
    }
}
