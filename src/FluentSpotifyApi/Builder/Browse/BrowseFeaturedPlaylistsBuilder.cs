using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Browse;

namespace FluentSpotifyApi.Builder.Browse
{
    internal class BrowseFeaturedPlaylistsBuilder : BuilderBase, IBrowseFeaturedPlaylistsBuilder
    {
        public BrowseFeaturedPlaylistsBuilder(ContextData contextData, IEnumerable<object> routeValuesPrefix) : base(contextData, routeValuesPrefix, "featured-playlists")
        {
        }

        public Task<FeaturedPlaylists> GetAsync(            
            string locale, 
            string country, 
            DateTime? timestamp, 
            int limit, 
            int offset,
            CancellationToken cancellationToken)
        {
            return this.GetAsync<FeaturedPlaylists>(
                cancellationToken,
                queryStringParameters: new { limit, offset },
                optionalQueryStringParameters: new { locale, country, timestamp = timestamp == null ? null : timestamp.Value.ToString("yyyy-MM-ddTHH:mm:ss") });
        }
    }
}
