using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Core.Internal.Extensions;
using FluentSpotifyApi.Model;

namespace FluentSpotifyApi.Builder.Artists
{
    internal class ArtistAlbumsBuilder : BuilderBase, IArtistAlbumsBuilder
    {
        public ArtistAlbumsBuilder(ContextData contextData, IEnumerable<object> routeValuesPrefix) : base(contextData, routeValuesPrefix, "albums")
        {
        }

        public Task<Page<SimpleAlbum>> GetAsync(
            IEnumerable<AlbumType> albumTypes, 
            IEnumerable<string> dynamicAlbumTypes, 
            string market, 
            int limit, 
            int offset,
            CancellationToken cancellationToken)
        {
            return this.GetAsync<Page<SimpleAlbum>>(
                cancellationToken,
                queryStringParameters: new { limit, offset },
                optionalQueryStringParameters: new
                {
                    album_type = albumTypes == null && dynamicAlbumTypes == null 
                        ? 
                        null 
                        : 
                        string.Join(",", albumTypes.EmptyIfNull().Select(item => item.GetDescription()).Concat(dynamicAlbumTypes.EmptyIfNull()).Distinct()),
                    market
                });
        }
    }
}
