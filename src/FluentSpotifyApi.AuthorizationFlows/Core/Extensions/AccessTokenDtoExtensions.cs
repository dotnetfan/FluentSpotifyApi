using FluentSpotifyApi.AuthorizationFlows.Core.Client.Token;
using FluentSpotifyApi.AuthorizationFlows.Core.Date;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Extensions
{
    /// <summary>
    /// The set of <see cref="AccessTokenDto"/> extensions.
    /// </summary>
    public static class AccessTokenDtoExtensions
    {
        /// <summary>
        /// Converts <see cref="AccessTokenDto"/> to <see cref="Model.AccessToken"/>.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="dateTimeOffsetProvider">The date time offset provider.</param>
        /// <returns></returns>
        public static Model.AccessToken ToModelToken(this AccessTokenDto accessToken, IDateTimeOffsetProvider dateTimeOffsetProvider)
        {
            return new Model.AccessToken(accessToken.Token, accessToken.ExpiresIn.ToExpiresAt(dateTimeOffsetProvider));
        }
    }
}
