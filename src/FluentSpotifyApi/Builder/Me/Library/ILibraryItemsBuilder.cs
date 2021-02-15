using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model;

namespace FluentSpotifyApi.Builder.Me.Library
{
    /// <summary>
    /// The builder for me/{entities} endpoint with IDs.
    /// </summary>
    /// <typeparam name="T">The item type.</typeparam>
    public interface ILibraryItemsBuilder<T>
    {
        /// <summary>
        /// Gets a list of items saved in the current Spotify user’s “Your Music” library.
        /// </summary>
        /// <param name="limit">The maximum number of objects to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">The index of the first object to return. Default: 0 (i.e., the first object). Use with <paramref name="limit"/> to get the next set of objects.</param>
        /// <param name="market">An ISO 3166-1 alpha-2 country code or the string <c>from_token</c>. Provide this parameter if you want to apply Track Relinking.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<Page<T>> GetAsync(int? limit = null, int? offset = null, string market = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Saves items to the current user’s “Your Music” library.
        /// </summary>
        /// <param name="ids">Item IDs. Maximum: 50 IDs.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task SaveAsync(IEnumerable<string> ids, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Removes items from the current user’s “Your Music” library.
        /// </summary>
        /// <param name="ids">Item IDs. Maximum: 50 IDs.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task RemoveAsync(IEnumerable<string> ids, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Checks if items are already saved in the current Spotify user’s “Your Music” library.
        /// </summary>
        /// <param name="ids">Item IDs. Maximum: 50 IDs.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<bool[]> CheckAsync(IEnumerable<string> ids, CancellationToken cancellationToken = default(CancellationToken));
    }
}
