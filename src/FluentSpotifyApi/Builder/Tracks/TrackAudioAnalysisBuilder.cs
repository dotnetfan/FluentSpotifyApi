using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Audio;

namespace FluentSpotifyApi.Builder.Tracks
{
    internal class TrackAudioAnalysisBuilder : EntityBuilderBase, ITrackAudioAnalysisBuilder
    {
        public TrackAudioAnalysisBuilder(RootBuilder root, string id)
            : base(root, "audio-analysis", id)
        {
        }

        public Task<AudioAnalysis> GetAsync(CancellationToken cancellationToken)
        {
            return this.GetAsync<AudioAnalysis>(cancellationToken);
        }
    }
}
