using FluentSpotifyApi.AuthorizationFlows.Core.Model;
using FluentSpotifyApi.Core.User;

namespace FluentSpotifyApi.AuthorizationFlows.Native.AuthorizationCode
{
    internal class AuthenticationTicket : IAuthenticationTicket
    {
        private readonly User user;

        public AuthenticationTicket(string refreshToken, AccessToken accessToken, UserClaims userClaims)
        {
            this.RefreshToken = refreshToken;
            this.AccessToken = accessToken;
            this.UserClaims = userClaims;
            this.user = new User(userClaims);
        }

        public string RefreshToken { get; }

        public AccessToken AccessToken { get; }

        public UserClaims UserClaims { get; }

        IUser IAuthenticationTicket.User => this.user;

        private class User : IUser
        {
            public User(UserClaims userClaims)
            {
                this.Id = userClaims[UserClaimTypes.Id].Value;
            }

            public string Id { get; }
        }
    }
}
