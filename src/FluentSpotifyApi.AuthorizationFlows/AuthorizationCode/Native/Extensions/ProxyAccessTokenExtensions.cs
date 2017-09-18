using FluentSpotifyApi.AuthorizationFlows.Core.Date;
using FluentSpotifyApi.AuthorizationFlows.Core.Extensions;
using FluentSpotifyApi.AuthorizationFlows.Core.Model;

namespace FluentSpotifyApi.AuthorizationFlows.AuthorizationCode.Native.Extensions
{
    internal static class ProxyAccessTokenExtensions
    {
        public static AccessToken ToModelToken(this ProxyAccessToken proxyAccessToken, IDateTimeOffsetProvider dateTimeOffsetProvider)
        {
            return new AccessToken(proxyAccessToken.Token, proxyAccessToken.ExpiresIn.ToExpiresAt(dateTimeOffsetProvider));
        }
    }
}
