using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentSpotifyApi.Core.Internal.Extensions
{
    /// <summary>
    /// Set of <see cref="IEnumerable{T}"/> extensions.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Empties if null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <returns></returns>
        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> enumerable)
        {
            return enumerable ?? Enumerable.Empty<T>();
        }

        /// <summary>
        /// Gets the query string.
        /// </summary>
        /// <param name="enumerable">The enumerable.</param>
        /// <returns></returns>
        public static string GetQueryString(this IEnumerable<KeyValuePair<string, object>> enumerable)
        {
            return string.Join("&", enumerable.EmptyIfNull().Select(item => $"{Uri.EscapeDataString(item.Key)}={item.Value.ToUrlString()}"));
        }
    }
}
