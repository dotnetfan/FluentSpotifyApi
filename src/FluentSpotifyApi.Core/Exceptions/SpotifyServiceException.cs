using System;

namespace FluentSpotifyApi.Core.Exceptions
{
    /// <summary>
    /// The exception that is thrown when a communication error with the Spotify service occurs.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class SpotifyServiceException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyServiceException"/> class.
        /// </summary>
        public SpotifyServiceException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyServiceException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public SpotifyServiceException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyServiceException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.
        /// </param>
        public SpotifyServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
