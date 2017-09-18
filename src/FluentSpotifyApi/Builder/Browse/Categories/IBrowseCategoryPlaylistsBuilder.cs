using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Messages;

namespace FluentSpotifyApi.Builder.Browse
{
    /// <summary>
    /// The builder for "browse/categories/{category_id}/playlists" endpoint.
    /// </summary>
    public interface IBrowseCategoryPlaylistsBuilder
    {
        /// <summary>
        /// Get a list of Spotify playlists tagged with a particular category.
        /// </summary>
        /// <param name="country">A country: an ISO 3166-1 alpha-2 country code.</param>
        /// <param name="limit">The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50. </param>
        /// <param name="offset">The index of the first item to return. Default: 0 (the first object).</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<SimplePlaylistsPageMessage> GetAsync(string country = null, int limit = 20, int offset = 0, CancellationToken cancellationToken = default(CancellationToken));
    }
}
