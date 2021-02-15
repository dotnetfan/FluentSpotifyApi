using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentSpotifyApi.Core.Utils
{
    /// <summary>
    /// The set of URI utilities intended for FluentSpotifyApi library usage.
    /// </summary>
    public static class SpotifyUriUtils
    {
        /// <summary>
        /// Gets relative URI from specified route values and query parameters.
        /// </summary>
        /// <param name="routeValues">The sequence of route values.</param>
        /// <param name="queryParams">The sequence of query parameters represented as key-value pairs.</param>
        /// <returns></returns>
        public static Uri GetRelativeUri(IEnumerable<object> routeValues, IEnumerable<KeyValuePair<string, object>> queryParams)
        {
            var relativeUri = GetPath(routeValues);
            if ((queryParams ?? Enumerable.Empty<KeyValuePair<string, object>>()).Any())
            {
                relativeUri += $"?{GetQueryString(queryParams)}";
            }

            return new Uri(relativeUri, UriKind.Relative);
        }

        /// <summary>
        /// Converts specified route values to URI path.
        /// </summary>
        /// <param name="routeValues">The sequence of route values.</param>
        /// <returns></returns>
        public static string GetPath(IEnumerable<object> routeValues)
        {
            return string.Join("/", (routeValues ?? Enumerable.Empty<object>()).Select(r => ConvertToUriString(r)));
        }

        /// <summary>
        /// Converts specified query parameters to URI query string.
        /// </summary>
        /// <param name="queryParams">The sequence of query parameters represented as key-value pairs.</param>
        /// <returns></returns>
        public static string GetQueryString(IEnumerable<KeyValuePair<string, object>> queryParams)
        {
            return string.Join("&", (queryParams ?? Enumerable.Empty<KeyValuePair<string, object>>()).Select(item => $"{Uri.EscapeDataString(item.Key)}={ConvertToUriString(item.Value)}"));
        }

        /// <summary>
        /// Converts specified value to URI escaped string.
        /// </summary>
        /// <param name="value">The value.</param>
        public static string ConvertToUriString(object value) => Uri.EscapeDataString(SpotifyObjectUtils.ConvertToCanonicalString(value));

        /// <summary>
        /// Converts specified array of bytes to Base64 encoded URI string.
        /// </summary>
        /// <param name="bytes">The array of bytes.</param>
        /// <returns></returns>
        public static string ConvertToBase64UriString(byte[] bytes) => Convert.ToBase64String(bytes).Replace("+", "-").Replace("/", "_").Replace("=", string.Empty);
    }
}
