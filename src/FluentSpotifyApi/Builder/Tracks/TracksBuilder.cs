using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Tracks;

namespace FluentSpotifyApi.Builder.Tracks
{
    internal class TracksBuilder : EntitiesBuilderBase, ITracksBuilder
    {
        public TracksBuilder(RootBuilder root, IEnumerable<string> ids)
            : base(root, "tracks", ids)
        {
        }

        public ITracksAudioFeaturesBuilder AudioFeatures => new TracksAudioFeaturesBuilder(this.CreateRootBuilder(), this.Sequence);

        public Task<TracksResponse> GetAsync(string market, CancellationToken cancellationToken)
        {
            return this.GetListAsync<TracksResponse>(cancellationToken, queryParams: new { market });
        }
    }
}
