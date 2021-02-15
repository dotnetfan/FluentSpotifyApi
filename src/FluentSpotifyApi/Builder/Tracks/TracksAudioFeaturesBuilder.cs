using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Audio;

namespace FluentSpotifyApi.Builder.Tracks
{
    internal class TracksAudioFeaturesBuilder : EntitiesBuilderBase, ITracksAudioFeaturesBuilder
    {
        public TracksAudioFeaturesBuilder(RootBuilder root, IEnumerable<string> ids)
            : base(root, "audio-features", ids)
        {
        }

        public Task<AudioFeaturesListResponse> GetAsync(CancellationToken cancellationToken)
        {
            return this.GetListAsync<AudioFeaturesListResponse>(cancellationToken);
        }
    }
}
