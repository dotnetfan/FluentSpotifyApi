using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Messages;

namespace FluentSpotifyApi.Builder.Tracks
{
    internal class TracksBuilder : EntitiesBuilderBase, ITracksBuilder
    {
        public TracksBuilder(ContextData contextData, string endpointName, IEnumerable<string> ids) : base(contextData, endpointName, ids)
        {
        }

        public ITracksAudioFeaturesBuilder AudioFeatures => new TracksAudioFeaturesBuilder(ContextData, this.Sequence);

        public Task<FullTracksMessage> GetAsync(string market, CancellationToken cancellationToken)
        {
            return this.GetListAsync<FullTracksMessage>(cancellationToken, optionalQueryStringParameters: new { market });
        }
    }
}
