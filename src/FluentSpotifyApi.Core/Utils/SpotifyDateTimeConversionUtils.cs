using System;

namespace FluentSpotifyApi.Core.Utils
{
    /// <summary>
    /// The set of DateTime conversion utilities intended for FluentSpotifyApi library usage.
    /// </summary>
    public static class SpotifyDateTimeConversionUtils
    {
        private static readonly DateTime UnixEpochDateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Converts specified value to epoch timestamp in milliseconds (i.e. number of milliseconds since 1/1/1970).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static long ToTimestampMilliseconds(DateTime value)
        {
            var utc = value.ToUniversalTime();
            return (long)(utc - UnixEpochDateTime).TotalMilliseconds;
        }

        /// <summary>
        /// Converts specified value representing epoch timestamp in milliseconds (i.e. number of milliseconds since 1/1/1970) to <see cref="DateTime"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DateTime FromTimestampMilliseconds(long value)
        {
            return UnixEpochDateTime.AddMilliseconds(value);
        }

        /// <summary>
        /// Converts specified value to ISO format without location (i.e. "yyyy-MM-ddTHH:mm:ss").
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string ToIsoWithUnspecifiedLocation(DateTime value) => value.ToString("yyyy-MM-ddTHH:mm:ss");
    }
}
