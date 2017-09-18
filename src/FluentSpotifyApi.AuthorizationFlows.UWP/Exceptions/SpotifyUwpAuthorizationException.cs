using FluentSpotifyApi.AuthorizationFlows.Exceptions;
using Windows.Security.Authentication.Web;

namespace FluentSpotifyApi.AuthorizationFlows.UWP.Exceptions
{
    /// <summary>
    /// The exception that is thrown when authorization fails.
    /// </summary>
    public class SpotifyUwpAuthorizationException : SpotifyAuthorizationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyUwpAuthorizationException"/> class.
        /// </summary>
        /// <param name="reason">The reason.</param>
        public SpotifyUwpAuthorizationException(WebAuthenticationStatus reason) : base($"Authorization exception with reason {reason} has occurred.")
        {
            this.Reason = reason;
        }

        /// <summary>
        /// Gets the reason.
        /// </summary>
        /// <value>
        /// The reason.
        /// </value>
        public WebAuthenticationStatus Reason { get; private set; }
    }
}
