using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Browse;

namespace FluentSpotifyApi.Builder.Browse
{
    internal class BrowseRecommendationsBuilder : BuilderBase, IBrowseRecommendationsBuilder
    {
        public BrowseRecommendationsBuilder(ContextData contextData) : base(contextData, "recommendations")
        {
        }

        public Task<Recommendations> GetAsync(
            int limit, 
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
                queryStringParameters: new { limit },
                optionalQueryStringParameters: new
                {
                    market,
                    seed_artists = GetList(seedArtists),
                    seed_genres = GetList(seedGenres),
                    seed_tracks = GetList(seedTracks),
                    attributes = builder.GetAttributes().ToList()
                });
        }

        private static string GetList(IEnumerable<string> ids)
        {
            return ids == null ? null : string.Join(",", ids);
        }
    }
}
