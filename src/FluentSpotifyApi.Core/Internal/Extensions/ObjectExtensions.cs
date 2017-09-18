using System;
using System.Globalization;

namespace FluentSpotifyApi.Core.Internal.Extensions
{
    internal static class ObjectExtensions
    {
        public static string ToUrlString(this object value) => Uri.EscapeDataString(value.ToInvariantString());

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
            else if (value is bool)
            {
                return (bool)value ? "true" : "false";
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
