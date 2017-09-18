using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Messages;

namespace FluentSpotifyApi.Builder.Tracks
{
    internal class TracksAudioFeaturesBuilder : EntitiesBuilderBase, ITracksAudioFeaturesBuilder
    {
        public TracksAudioFeaturesBuilder(ContextData contextData, IEnumerable<string> ids) : base(contextData, "audio-features", ids)
        {
        }

        public Task<AudioFeaturesListMessage> GetAsync(CancellationToken cancellationToken)
        {
            return this.GetListAsync<AudioFeaturesListMessage>(cancellationToken);
        }
    }
}
