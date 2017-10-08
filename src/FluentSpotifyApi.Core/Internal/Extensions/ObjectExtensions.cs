using System;
using System.Globalization;

namespace FluentSpotifyApi.Core.Internal.Extensions
{
    /// <summary>
    /// The set of <see cref="Object"/> extensions.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Converts specified value to URL escaped string.
        /// </summary>
        /// <param name="value">The value.</param>
        public static string ToUrlString(this object value) => Uri.EscapeDataString(value.ToInvariantString());

        /// <summary>
        /// Converts specified value to invariant string.
        /// </summary>
        /// <param name="value">The value.</param>
        public static string ToInvariantString(this object value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            else if (value is Uri uri)
            {
                return uri.AbsoluteUri;
            }
            else if (value is bool boolValue)
            {
                return boolValue ? "true" : "false";
            }
            else if (value is IFormattable formattable)
            {
                return formattable.ToString(null, CultureInfo.InvariantCulture);
            }
            else
            {
                return value.ToString();
            }
        }
    }
}
