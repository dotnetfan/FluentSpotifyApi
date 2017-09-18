using System.Collections.Generic;

namespace FluentSpotifyApi.Builder.Albums
{
    internal static class Factory
    {
        private const string EndpointName = "albums";

        public static IAlbumBuilder CreateAlbumBuilder(ContextData contextData, string id)
        {
            return new AlbumBuilder(contextData, EndpointName, id);
        }

        public static IAlbumsBuilder CreateAlbumsBuilder(ContextData contextData, IEnumerable<string> ids)
        {
            return new AlbumsBuilder(contextData, EndpointName, ids);
        }
    }
}
