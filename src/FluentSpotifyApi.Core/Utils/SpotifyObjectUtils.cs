using System;
using System.Collections.Generic;
using System.Globalization;

namespace FluentSpotifyApi.Core.Utils
{
    /// <summary>
    /// The set of object utilities intended for FluentSpotifyApi library usage.
    /// </summary>
    public static class SpotifyObjectUtils
    {
        private static readonly HashSet<Type> NumericTypes = new HashSet<Type>
        {
            typeof(int),  typeof(double),  typeof(decimal),
            typeof(long), typeof(short),   typeof(sbyte),
            typeof(byte), typeof(ulong),   typeof(ushort),
            typeof(uint), typeof(float)
        };

        /// <summary>
        /// Converts specified value to canonical string representation.
        /// </summary>
        /// <param name="value">The value.</param>
        public static string ConvertToCanonicalString(object value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            else if (value is string stringValue)
            {
                return stringValue;
            }
            else if (value is bool boolValue)
            {
                return boolValue ? "true" : "false";
            }
            else if (value is Uri uri)
            {
                return uri.AbsoluteUri;
            }
            else if (value.GetType() is var type && NumericTypes.Contains(Nullable.GetUnderlyingType(type) ?? type))
            {
                return ((IFormattable)value).ToString(null, CultureInfo.InvariantCulture);
            }
            else
            {
                throw new InvalidOperationException($"Value of '{type}' cannot be converted to canonical string.");
            }
        }
    }
}
