using System;

namespace FluentSpotifyApi.Core.Exceptions
{
    /// <summary>
    /// The exception that is thrown when a communication error with the Spotify service occurs.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public abstract class SpotifyCommunicationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyCommunicationException"/> class.
        /// </summary>
        public SpotifyCommunicationException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyCommunicationException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="clientType">The client type.</param>
        public SpotifyCommunicationException(string message, Type clientType)
            : base(message)
        {
            this.ClientType = clientType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyCommunicationException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="clientType">The client type.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.
        /// </param>
        public SpotifyCommunicationException(string message, Type clientType, Exception innerException)
            : base(message, innerException)
        {
            this.ClientType = clientType;
        }

        /// <summary>
        /// Gets the client type from where the exception was thrown.
        /// </summary>
        public Type ClientType { get; }
    }
}
