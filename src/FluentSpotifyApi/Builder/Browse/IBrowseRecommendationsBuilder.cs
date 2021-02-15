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
        /// Creates a playlist-style listening experience based on seed artists, tracks and genres.
        /// </summary>
        /// <param name="limit">
        /// The target size of the list of recommended tracks.
        /// For seeds with unusually small pools or when highly restrictive filtering is applied, it may be impossible to generate the requested number of recommended tracks.
        /// Debugging information for such cases is available in the response. Default: 20. Minimum: 1. Maximum: 100.</param>
        /// <param name="market">
        /// An ISO 3166-1 alpha-2 country code or the string <c>from_token</c>. Provide this parameter if you want to apply Track Relinking.
        /// Because <see cref="ITuneableTrackAttributeBuilder{T}.Min(T)"/>, <see cref="ITuneableTrackAttributeBuilder{T}.Max(T)"/> and <see cref="ITuneableTrackAttributeBuilder{T}.Target(T)"/>
        /// are applied to pools before relinking, the generated results may not precisely match the filters applied.
        /// Original, non-relinked tracks are available via the <see cref="Model.Tracks.TrackBase.LinkedFrom"/> attribute of the relinked track response.
        /// </param>
        /// <param name="seedArtists">
        /// The list of Spotify IDs for seed artists.
        /// Up to 5 seed values may be provided in any combination of <paramref name="seedArtists"/>, <paramref name="seedTracks"/> and <paramref name="seedGenres"/>.
        /// </param>
        /// <param name="seedGenres">
        /// List of any genres in the set of available genre seeds.
        /// Up to 5 seed values may be provided in any combination of <paramref name="seedArtists"/>, <paramref name="seedTracks"/> and <paramref name="seedGenres"/>.
        /// </param>
        /// <param name="seedTracks">
        /// The list of Spotify IDs for a seed track.
        /// Up to 5 seed values may be provided in any combination of <paramref name="seedArtists"/>, <paramref name="seedTracks"/> and <paramref name="seedGenres"/>.
        /// </param>
        /// <param name="buildTunableTrackAttributes">The action for building tunable track attributes.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<Recommendations> GetAsync(
            int? limit = null,
            string market = null,
            IEnumerable<string> seedArtists = null,
            IEnumerable<string> seedGenres = null,
            IEnumerable<string> seedTracks = null,
            Action<ITuneableTrackAttributesBuilder> buildTunableTrackAttributes = null,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}
