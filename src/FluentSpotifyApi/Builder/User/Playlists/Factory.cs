using System.Collections.Generic;

namespace FluentSpotifyApi.Builder.User.Playlists
{
    internal static class Factory
    {
        private const string EndpointName = "playlists";

        public static IPlaylistBuilder CreatePlaylistBuilder(ContextData contextData, IEnumerable<object> routeValuesPrefix, string id)
        {
            return new PlaylistBuilder(contextData, routeValuesPrefix, EndpointName, id);
        }

        public static IPlaylistsBuilder CreatePlaylistsBuilder(ContextData contextData, IEnumerable<object> routeValuesPrefix)
        {
            return new PlaylistsBuilder(contextData, routeValuesPrefix, EndpointName);
        }
    }
}
