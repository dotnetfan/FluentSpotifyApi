using System;
using FluentSpotifyApi.Core.Utils;

namespace FluentSpotifyApi.Extensions
{
    internal static class DateTimeExtensions
    {
        public static long ToTimestampMilliseconds(this DateTime value) => SpotifyDateTimeConversionUtils.ToTimestampMilliseconds(value);

        public static string ToIsoWithUnspecifiedLocation(this DateTime value) => SpotifyDateTimeConversionUtils.ToIsoWithUnspecifiedLocation(value);
    }
}
