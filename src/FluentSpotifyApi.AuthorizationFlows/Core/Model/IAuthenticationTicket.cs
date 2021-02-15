using FluentSpotifyApi.Core.User;

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
        IUser User { get; }

        /// <summary>
        /// Gets the access token.
        /// </summary>
        AccessToken AccessToken { get; }
    }
}
