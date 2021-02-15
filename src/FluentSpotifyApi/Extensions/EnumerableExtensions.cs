using System.Collections.Generic;

namespace FluentSpotifyApi.Extensions
{
    internal static class EnumerableExtensions
    {
        public static string JoinWithComma(this IEnumerable<string> values) => values == null ? null : string.Join(",", values);
    }
}
