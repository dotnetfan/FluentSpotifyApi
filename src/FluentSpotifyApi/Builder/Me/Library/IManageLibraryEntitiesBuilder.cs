using System.Threading;
using System.Threading.Tasks;

namespace FluentSpotifyApi.Builder.Me.Library
{
    /// <summary>
    /// The builder for me/entities endpoint with IDs.
    /// </summary>
    public interface IManageLibraryEntitiesBuilder
    {
        /// <summary>
        /// Save one or more tracks or albums to the current user’s “Your Music” library.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task SaveAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Remove one or more tracks or albums from the current user’s “Your Music” library.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task RemoveAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Check if one or more tracks or albums is already saved in the current Spotify user’s “Your Music” library.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<bool[]> CheckAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
