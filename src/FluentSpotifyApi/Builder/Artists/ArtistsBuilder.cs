using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Messages;

namespace FluentSpotifyApi.Builder.Artists
{
    internal class ArtistsBuilder : EntitiesBuilderBase, IArtistsBuilder
    {
        public ArtistsBuilder(ContextData contextData, string endpointName, IEnumerable<string> ids) : base(contextData, endpointName, ids)
        {
        }

        public async Task<FullArtistsMessage> GetAsync(CancellationToken cancellationToken)
        {
            return await this.GetListAsync<FullArtistsMessage>(cancellationToken);
        }
    }
}
