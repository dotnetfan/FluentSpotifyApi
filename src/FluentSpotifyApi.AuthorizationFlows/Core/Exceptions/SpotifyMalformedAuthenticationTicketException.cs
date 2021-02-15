using System;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Exceptions
{
    /// <summary>
    /// The exception that is thrown when one of the Spotify User Authorization Flow is used and stored authentication ticket is not valid.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class SpotifyMalformedAuthenticationTicketException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyMalformedAuthenticationTicketException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public SpotifyMalformedAuthenticationTicketException(string message)
            : base(message)
        {
        }
    }
}
