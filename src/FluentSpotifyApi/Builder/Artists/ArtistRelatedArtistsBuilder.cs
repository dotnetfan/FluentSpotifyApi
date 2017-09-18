using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Messages;

namespace FluentSpotifyApi.Builder.Artists
{
    internal class ArtistRelatedArtistsBuilder : BuilderBase, IArtistRelatedArtistsBuilder
    {
        public ArtistRelatedArtistsBuilder(ContextData contextData, IEnumerable<object> routeValuesPrefix) : base(contextData, routeValuesPrefix, "related-artists")
        {
        }

        public Task<FullArtistsMessage> GetAsync(CancellationToken cancellationToken)
        {
            return this.GetAsync<FullArtistsMessage>(cancellationToken);
        }
    }
}
