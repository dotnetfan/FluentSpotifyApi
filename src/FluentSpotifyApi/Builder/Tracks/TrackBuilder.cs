using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Tracks;

namespace FluentSpotifyApi.Builder.Tracks
{
    internal class TrackBuilder : EntityBuilderBase, ITrackBuilder
    {
        public TrackBuilder(RootBuilder root, string id)
            : base(root, "tracks", id)
        {
        }

        public ITrackAudioAnalysisBuilder AudioAnalysis => new TrackAudioAnalysisBuilder(this.CreateRootBuilder(), this.Id);

        public ITrackAudioFeaturesBuilder AudioFeatures => new TrackAudioFeaturesBuilder(this.CreateRootBuilder(), this.Id);

        public Task<Track> GetAsync(string market, CancellationToken cancellationToken)
        {
            return this.GetAsync<Track>(cancellationToken, queryParams: new { market });
        }
    }
}
