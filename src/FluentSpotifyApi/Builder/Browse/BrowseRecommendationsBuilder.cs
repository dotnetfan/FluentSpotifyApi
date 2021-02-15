using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Extensions;
using FluentSpotifyApi.Model.Browse;

namespace FluentSpotifyApi.Builder.Browse
{
    internal class BrowseRecommendationsBuilder : BuilderBase, IBrowseRecommendationsBuilder
    {
        public BrowseRecommendationsBuilder(RootBuilder root)
            : base(root, "recommendations".Yield())
        {
        }

        public Task<Recommendations> GetAsync(
            int? limit,
            string market,
            IEnumerable<string> seedArtists,
            IEnumerable<string> seedGenres,
            IEnumerable<string> seedTracks,
            Action<ITuneableTrackAttributesBuilder> buildTunableTrackAttributes,
            CancellationToken cancellationToken)
        {
            var builder = new TuneableTrackAttributesBuilder();
            buildTunableTrackAttributes?.Invoke(builder);

            return this.GetAsync<Recommendations>(
                cancellationToken,
                queryParams: new
                {
                    limit,
                    market,
                    seed_artists = seedArtists.JoinWithComma(),
                    seed_genres = seedGenres.JoinWithComma(),
                    seed_tracks = seedTracks.JoinWithComma(),
                    attributes = builder.GetAttributes().ToList()
                });
        }
    }
}
