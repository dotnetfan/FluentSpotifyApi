using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Model
{
    /// <summary>
    /// The authentication ticket interface.
    /// </summary>
    public interface IAuthenticationTicket
    {
        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        IUser User { get; }

        /// <summary>
        /// Gets the access token.
        /// </summary>
        /// <value>
        /// The access token.
        /// </value>
        AccessToken AccessToken { get; }
    }
}
