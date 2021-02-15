using System;

namespace FluentSpotifyApi.Core.Utils
{
    /// <summary>
    /// The set of TimeSpan conversion utilities intended for FluentSpotifyApi library usage.
    /// </summary>
    public static class SpotifyTimeSpanConversionUtils
    {
        /// <summary>
        /// Converts specified value to whole milliseconds.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static int ToWholeMilliseconds(TimeSpan value) => checked((int)value.TotalMilliseconds);

        /// <summary>
        /// Converts specified value representing time interval in whole milliseconds to <see cref="TimeSpan"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static TimeSpan FromWholeMilliseconds(int value) => TimeSpan.FromMilliseconds(value);
    }
}
