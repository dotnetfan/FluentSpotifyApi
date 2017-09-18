using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Audio;

namespace FluentSpotifyApi.Builder.Tracks
{
    internal class TrackAudioFeaturesBuilder : EntityBuilderBase, ITrackAudioFeaturesBuilder
    {
        public TrackAudioFeaturesBuilder(ContextData contextData, string id) : base(contextData, "audio-features", id)
        {
        }

        public Task<AudioFeatures> GetAsync(CancellationToken cancellationToken)
        {
            return this.GetAsync<AudioFeatures>(cancellationToken);
        }
    }
}
