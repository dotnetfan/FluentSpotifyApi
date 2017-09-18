using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Audio;

namespace FluentSpotifyApi.Builder.Tracks
{
    internal class TrackAudioAnalysisBuilder : EntityBuilderBase, ITrackAudioAnalysisBuilder
    {
        public TrackAudioAnalysisBuilder(ContextData contextData, string id) : base(contextData, "audio-analysis", id)
        {
        }

        public Task<AudioAnalysis> GetAsync(CancellationToken cancellationToken)
        {
            return this.GetAsync<AudioAnalysis>(cancellationToken);
        }
    }
}
