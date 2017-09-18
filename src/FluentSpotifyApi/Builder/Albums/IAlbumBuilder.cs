using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model;

namespace FluentSpotifyApi.Builder.Albums
{
    /// <summary>
    /// The builder for "albums/{id}" endpoint.
    /// </summary>
    public interface IAlbumBuilder
    {
        /// <summary>
        /// Gets builder for "albums/{id}/tracks" endpoint.
        /// </summary>
        IAlbumTracksBuilder Tracks { get; }

        /// <summary>
        /// Get Spotify catalog information for a single album.
        /// </summary>
        /// <param name="market">An ISO 3166-1 alpha-2 country code. </param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<FullAlbum> GetAsync(string market = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}
