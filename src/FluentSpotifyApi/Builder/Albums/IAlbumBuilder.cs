using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Albums;

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
        /// Gets Spotify catalog information for a single album.
        /// </summary>
        /// <param name="market">The market you’d like to request. Synonym for <c>country</c>.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<Album> GetAsync(string market = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}
