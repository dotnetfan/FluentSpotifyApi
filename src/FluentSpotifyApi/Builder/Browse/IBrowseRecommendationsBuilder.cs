using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Browse;

namespace FluentSpotifyApi.Builder.Browse
{
    /// <summary>
    /// The builder for "recommendations" endpoint.
    /// </summary>
    public interface IBrowseRecommendationsBuilder
    {
        /// <summary>
        /// Create a playlist-style listening experience based on seed artists, tracks and genres.
        /// </summary>
        /// <param name="limit">The target size of the list of recommended tracks. Default: 20. Minimum: 1. Maximum: 100.</param>
        /// <param name="market">An ISO 3166-1 alpha-2 country code.</param>
        /// <param name="seedArtists">The list of Spotify IDs for seed artists. Maximum: 5.</param>
        /// <param name="seedGenres">The list of genres. Maximum: 5.</param>
        /// <param name="seedTracks">The list of Spotify IDs for a seed track. Maximum: 5</param>
        /// <param name="buildTunableTrackAttributes">The action for building tunable track attributes.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<Recommendations> GetAsync(
            int limit = 20, 
            string market = null, 
            IEnumerable<string> seedArtists = null, 
            IEnumerable<string> seedGenres = null,
            IEnumerable<string> seedTracks = null, 
            Action<ITuneableTrackAttributesBuilder> buildTunableTrackAttributes = null,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}
