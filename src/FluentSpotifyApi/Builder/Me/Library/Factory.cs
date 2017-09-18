using System.Collections.Generic;
using FluentSpotifyApi.Model;

namespace FluentSpotifyApi.Builder.Me.Library
{
    internal static class Factory
    {
        private const string AlbumsEndpointName = "albums";

        private const string TracksEndpointName = "tracks";

        public static IGetLibraryEntitiesBuilder<SavedAlbum> CreateGetLibraryAlbumsBuilder(ContextData contextData, IEnumerable<object> routeValuesPrefix)
        {
            return new GetLibraryEntitiesBuilder<SavedAlbum>(contextData, routeValuesPrefix, AlbumsEndpointName);
        }

        public static IGetLibraryEntitiesBuilder<SavedTrack> CreateGetLibraryTracksBuilder(ContextData contextData, IEnumerable<object> routeValuesPrefix)
        {
            return new GetLibraryEntitiesBuilder<SavedTrack>(contextData, routeValuesPrefix, TracksEndpointName);
        }

        public static IManageLibraryEntitiesBuilder CreateManageLibraryAlbumsBuilder(ContextData contextData, IEnumerable<object> routeValuesPrefix, IEnumerable<string> ids)
        {
            return new ManageLibraryEntitiesBuilder(contextData, routeValuesPrefix, AlbumsEndpointName, ids);
        }

        public static IManageLibraryEntitiesBuilder CreateManageLibraryTracksBuilder(ContextData contextData, IEnumerable<object> routeValuesPrefix, IEnumerable<string> ids)
        {
            return new ManageLibraryEntitiesBuilder(contextData, routeValuesPrefix, TracksEndpointName, ids);
        }
    }
}
