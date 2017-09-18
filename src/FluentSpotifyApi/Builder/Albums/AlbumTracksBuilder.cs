using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model;

namespace FluentSpotifyApi.Builder.Albums
{
    internal class AlbumTracksBuilder : BuilderBase, IAlbumTracksBuilder
    {
        public AlbumTracksBuilder(ContextData contextData, IEnumerable<object> routeValuesPrefix) : base(contextData, routeValuesPrefix, "tracks")
        {
        }

        public Task<Page<SimpleTrack>> GetAsync(int limit, int offset, string market, CancellationToken cancellationToken)
        {
            return this.GetAsync<Page<SimpleTrack>>(cancellationToken, queryStringParameters: new { limit, offset }, optionalQueryStringParameters: new { market });
        }
    }
}
