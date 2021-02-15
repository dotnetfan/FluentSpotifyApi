using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.Builder.Playlists
{
    /// <summary>
    /// The builder for "playlists/{id}/images" endpoint.
    /// </summary>
    public interface IPlaylistImagesBuilder
    {
        /// <summary>
        /// Gets the current image associated with a specific playlist.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<Image[]> GetAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Replaces the image used to represent a specific playlist.
        /// </summary>
        /// <param name="jpegCover">The JPEG cover byte array.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task UpdateAsync(byte[] jpegCover, CancellationToken cancellationToken = default(CancellationToken));
    }
}
