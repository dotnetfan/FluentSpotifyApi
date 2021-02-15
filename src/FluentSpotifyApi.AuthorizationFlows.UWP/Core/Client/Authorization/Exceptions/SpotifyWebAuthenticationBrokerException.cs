using FluentSpotifyApi.AuthorizationFlows.Core.Client.Authorization.Exceptions;
using Windows.Security.Authentication.Web;

namespace FluentSpotifyApi.AuthorizationFlows.UWP.Core.Client.Authorization.Exceptions
{
    /// <summary>
    /// The exception that is thrown when authorization fails.
    /// </summary>
    public class SpotifyWebAuthenticationBrokerException : SpotifyAuthorizationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyWebAuthenticationBrokerException"/> class.
        /// </summary>
        /// <param name="reason">The reason.</param>
        public SpotifyWebAuthenticationBrokerException(WebAuthenticationStatus reason)
            : base($"Authorization exception with reason '{reason}' has occurred.")
        {
            this.Reason = reason;
        }

        /// <summary>
        /// Gets the reason.
        /// </summary>
        public WebAuthenticationStatus Reason { get; }
    }
}
