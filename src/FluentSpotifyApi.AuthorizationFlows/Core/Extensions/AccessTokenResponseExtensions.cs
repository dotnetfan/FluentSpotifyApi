using FluentSpotifyApi.AuthorizationFlows.Core.Client.Token;
using FluentSpotifyApi.AuthorizationFlows.Core.Time;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Extensions
{
    /// <summary>
    /// The set of <see cref="AccessTokenResponse"/> extensions.
    /// </summary>
    public static class AccessTokenResponseExtensions
    {
        /// <summary>
        /// Gets instance of <see cref="Model.AccessToken"/> from specified <paramref name="accessTokenResponse"/>.
        /// </summary>
        /// <param name="accessTokenResponse">The access token response.</param>
        /// <param name="clock">The clock instance.</param>
        /// <returns></returns>
        public static Model.AccessToken GetAccessTokenModel(this IAccessTokenResponse accessTokenResponse, IClock clock)
        {
            return new Model.AccessToken(accessTokenResponse.AccessToken, clock.GetUtcNow().AddSeconds(accessTokenResponse.ExpiresIn));
        }
    }
}
