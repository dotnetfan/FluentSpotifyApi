using FluentSpotifyApi.AuthorizationFlows.Core.Date;
using FluentSpotifyApi.AuthorizationFlows.Core.Model;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Extensions
{
    /// <summary>
    /// The set of <see cref="AccessToken"/> extensions.
    /// </summary>
    public static class AccessTokenExtensions
    {
        /// <summary>
        /// Checks whether the access token is valid.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="dateTimeOffsetProvider">The date time offset provider.</param>
        /// <returns>
        /// </returns>
        public static bool IsValid(
            this AccessToken accessToken, 
            IDateTimeOffsetProvider dateTimeOffsetProvider)
        {
            return accessToken != null && dateTimeOffsetProvider.GetUtcNow() < accessToken.ExpiresAt;
        }
    }
}
