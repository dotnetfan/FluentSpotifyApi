using FluentSpotifyApi.AuthorizationFlows.Core.Model;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.AuthorizationFlows.AuthorizationCode.Native
{
    /// <summary>
    /// The authentication ticket.
    /// </summary>
    /// <seealso cref="FluentSpotifyApi.AuthorizationFlows.Core.Model.IAuthenticationTicket" />
    public class AuthenticationTicket : IAuthenticationTicket
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationTicket"/> class.
        /// </summary>
        /// <param name="authorizationKey">The authorization key.</param>
        /// <param name="accessToken">The access token.</param>
        /// <param name="user">The user.</param>
        public AuthenticationTicket(string authorizationKey, AccessToken accessToken, PrivateUser user)
        {
            this.AuthorizationKey = authorizationKey;
            this.AccessToken = accessToken;
            this.User = user;
        }

        /// <summary>
        /// Gets the authorization key that is used for getting access tokens from the token proxy service.
        /// </summary>
        public string AuthorizationKey { get; }

        /// <summary>
        /// Gets the access token.
        /// </summary>
        public AccessToken AccessToken { get; }

        /// <summary>
        /// Gets the user.
        /// </summary>
        public PrivateUser User { get; }

        /// <summary>
        /// Gets the user.
        /// </summary>
        IUser IAuthenticationTicket.User => this.User;
    }
}
