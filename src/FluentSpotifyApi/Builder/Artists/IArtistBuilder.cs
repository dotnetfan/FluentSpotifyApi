using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model;

namespace FluentSpotifyApi.Builder.Artists
{
    /// <summary>
    /// The builder for "artists/{id}" endpoint.
    /// </summary>
    public interface IArtistBuilder
    {
        /// <summary>
        /// Gets builder for "artists/{id}/albums" endpoint.
        /// </summary>
        IArtistAlbumsBuilder Albums { get; }

        /// <summary>
        /// Gets builder for "artists/{id}/top-tracks" endpoint.
        /// </summary>
        IArtistTopTracksBuilder TopTracks { get; }

        /// <summary>
        /// Gets builder for "artists/{id}/related-artists" endpoint.
        /// </summary>
        IArtistRelatedArtistsBuilder RelatedArtists { get; }

        /// <summary>
        /// Get Spotify catalog information for a single artist identified by their unique Spotify ID.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<FullArtist> GetAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
