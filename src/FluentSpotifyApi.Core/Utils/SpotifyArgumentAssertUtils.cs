using System;

namespace FluentSpotifyApi.Core.Utils
{
    /// <summary>
    /// The set of argument assert utilities intended for FluentSpotifyApi library usage.
    /// </summary>
    public static class SpotifyArgumentAssertUtils
    {
        /// <summary>
        /// Throws <see cref="ArgumentNullException"/> when specified value is null.
        /// </summary>
        /// <typeparam name="T">The value type.</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="paramName">The input parameter name.</param>
        public static void ThrowIfNull<T>(T value, string paramName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        /// <summary>
        /// Throws <see cref="ArgumentNullException"/> when specified value is null.
        /// Throws <see cref="ArgumentException"/> when specified value is empty.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <param name="paramName">The input parameter name.</param>
        public static void ThrowIfNullOrEmpty(string value, string paramName)
        {
            ThrowIfNull(value, paramName);

            if (value.Length == 0)
            {
                throw new ArgumentException("Value cannot be empty.", paramName);
            }
        }
    }
}
