using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace FluentSpotifyApi.Builder.User.Playlists.Tracks
{
    internal static class Factory
    {
        private const string EndpointName = "tracks";

        public static IPlaylistTracksBuilder CreatePlaylistTracksBuilder(ContextData contextData, IEnumerable<object> routeValuesPrefix)
        {
            return new PlaylistTracksBuilder(contextData, routeValuesPrefix, EndpointName);
        }

        public static IPlaylistTrackIdsBuilder CreatePlaylistTrackIdsBuilder(ContextData contextData, IEnumerable<object> routeValuesPrefix, IEnumerable<string> ids)
        {
            return new PlaylistTrackIdsBuilder(contextData, routeValuesPrefix, EndpointName, ids);
        }

        [SuppressMessage("Microsoft.StyleCop.CSharp.SpacingRules", "SA1009:ClosingParenthesisMustBeSpacedCorrectly", Justification = "C# 7 Tuples")]
        public static IPlaylistTrackIdsWithPositionsBuilder CreatePlaylistTrackIdsWithPositionsBuilder(
            ContextData contextData, 
            IEnumerable<object> routeValuesPrefix, 
            IEnumerable<(string Id, int[] Positions)> idsWithPositions)
        {
            return new PlaylistTrackIdsWithPositionsBuilder(contextData, routeValuesPrefix, EndpointName, idsWithPositions);
        }

        public static IPlaylistTrackPositionsBuilder CreatePlaylistTrackPositionsBuilder(ContextData contextData, IEnumerable<object> routeValuesPrefix, IEnumerable<int> positions)
        {
            return new PlaylistTrackPositionsBuilder(contextData, routeValuesPrefix, EndpointName, positions);
        }
    }
}
