using System.Collections.Generic;

namespace FluentSpotifyApi.Builder.Artists
{
    internal static class Factory
    {
        private const string EndpointName = "artists";

        public static IArtistBuilder CreateArtistBuilder(ContextData contextData, string id)
        {
            return new ArtistBuilder(contextData, EndpointName, id);
        }

        public static IArtistsBuilder CreateArtistsBuilder(ContextData contextData, IEnumerable<string> ids)
        {
            return new ArtistsBuilder(contextData, EndpointName, ids);
        }
    }
}
