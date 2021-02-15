using System.Collections.Generic;

namespace FluentSpotifyApi.Extensions
{
    internal static class ObjectExtensions
    {
        public static IEnumerable<T> Yield<T>(this T item)
        {
            yield return item;
        }
    }
}
