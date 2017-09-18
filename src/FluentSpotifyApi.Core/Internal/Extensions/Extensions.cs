using System.Collections.Generic;
using Newtonsoft.Json;

namespace FluentSpotifyApi.Core.Internal.Extensions
{
    /// <summary>
    /// Set of generic extensions.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Yields the specified item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public static IEnumerable<T> Yield<T>(this T item)
        {
            yield return item;
        }

        /// <summary>
        /// Clones the specified source using JSON serializer.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static T CloneUsingJsonSerializer<T>(this T source) where T : class
        {
            if (source == null)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source));
        }
    }
}
