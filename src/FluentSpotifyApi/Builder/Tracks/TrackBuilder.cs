using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model;

namespace FluentSpotifyApi.Builder.Tracks
{
    internal class TrackBuilder : EntityBuilderBase, ITrackBuilder
    {
        public TrackBuilder(ContextData contextData, string endpointName, string id) : base(contextData, endpointName, id)
        {
        }

        public ITrackAudioAnalysisBuilder AudioAnalysis => new TrackAudioAnalysisBuilder(ContextData, Id);

        public ITrackAudioFeaturesBuilder AudioFeatures => new TrackAudioFeaturesBuilder(ContextData, Id);

        public Task<FullTrack> GetAsync(string market, CancellationToken cancellationToken)
        {
            return this.GetAsync<FullTrack>(cancellationToken, optionalQueryStringParameters: new { market });
        }
    }
}
