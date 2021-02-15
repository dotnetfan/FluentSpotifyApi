using System;
using FluentSpotifyApi.Core.Utils;

namespace FluentSpotifyApi.Extensions
{
    internal static class TimeSpanExtensions
    {
        public static int ToWholeMilliseconds(this TimeSpan value) => SpotifyTimeSpanConversionUtils.ToWholeMilliseconds(value);
    }
}
