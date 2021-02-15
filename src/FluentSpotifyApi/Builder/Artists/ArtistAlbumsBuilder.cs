using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Extensions;
using FluentSpotifyApi.Model;
using FluentSpotifyApi.Model.Albums;

namespace FluentSpotifyApi.Builder.Artists
{
    internal class ArtistAlbumsBuilder : BuilderBase, IArtistAlbumsBuilder
    {
        public ArtistAlbumsBuilder(BuilderBase parent)
            : base(parent, "albums".Yield())
        {
        }

        public Task<Page<SimplifiedAlbum>> GetAsync(
            IEnumerable<AlbumType> includeGroups,
            string market,
            int? limit,
            int? offset,
            CancellationToken cancellationToken)
        {
            return this.GetAsync<Page<SimplifiedAlbum>>(
                cancellationToken,
                queryParams: new
                {
                    include_groups = includeGroups?.Select(item => item.GetEnumMemberValue()).JoinWithComma(),
                    market,
                    limit,
                    offset
                });
        }
    }
}
