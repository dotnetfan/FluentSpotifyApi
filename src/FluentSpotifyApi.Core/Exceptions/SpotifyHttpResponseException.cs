using System;
using System.Net;

namespace FluentSpotifyApi.Core.Exceptions
{
    /// <summary>
    /// The exception that is thrown when a response with error <see cref="HttpStatusCode"/> is returned from the Spotify service.
    /// </summary>
    public class SpotifyHttpResponseException : SpotifyCommunicationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyHttpResponseException" /> class.
        /// </summary>
        /// <param name="clientType">The client type.</param>
        /// <param name="errorCode">The error status code.</param>
        /// <param name="content">The content.</param>
        public SpotifyHttpResponseException(Type clientType, HttpStatusCode errorCode, string content)
            : this("Error response has been returned from the server.", clientType, errorCode, content)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyHttpResponseException" /> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="clientType">The client type.</param>
        /// <param name="errorCode">The error status code.</param>
        /// <param name="content">The content.</param>
        public SpotifyHttpResponseException(string message, Type clientType, HttpStatusCode errorCode, string content)
            : base(FormatMessage(message, errorCode, content), clientType)
        {
            this.ErrorCode = errorCode;
            this.Content = content;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyHttpResponseException" /> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="clientType">The client type.</param>
        /// <param name="errorCode">The error status code.</param>
        /// <param name="content">The content.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.
        /// </param>
        public SpotifyHttpResponseException(string message, Type clientType, HttpStatusCode errorCode, string content, Exception innerException)
            : base(FormatMessage(message, errorCode, content), clientType, innerException)
        {
            this.ErrorCode = errorCode;
            this.Content = content;
        }

        /// <summary>
        /// Gets the status code.
        /// </summary>
        public HttpStatusCode ErrorCode { get; }

        /// <summary>
        /// Gets the content.
        /// </summary>
        public string Content { get; }

        private static string FormatMessage(string message, HttpStatusCode errorCode, string content)
        {
            var result = $"{message} Code: {(int)errorCode}";

            if (!string.IsNullOrEmpty(content))
            {
                result += $", Content:{Environment.NewLine}{content.Substring(0, Math.Min(content.Length, 512))}";
            }

            return result;
        }
    }
}
