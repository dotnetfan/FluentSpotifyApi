using System;
using FluentSpotifyApi.AuthorizationFlows.Core.Date;

namespace FluentSpotifyApi.AuthorizationFlows.Core.Extensions
{
    internal static class IntExtensions
    {
        public static DateTimeOffset ToExpiresAt(this int expiresIn, IDateTimeOffsetProvider dateTimeOffsetProvider)
        {
            return dateTimeOffsetProvider.GetUtcNow().AddSeconds(expiresIn);
        }
    }
}
