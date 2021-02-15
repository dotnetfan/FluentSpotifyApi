using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Playlists;

namespace FluentSpotifyApi.Builder.Browse
{
    /// <summary>
    /// The builder for "browse/categories/{category_id}/playlists" endpoint.
    /// </summary>
    public interface IBrowseCategoryPlaylistsBuilder
    {
        /// <summary>
        /// Gets a list of Spotify playlists tagged with a particular category.
        /// </summary>
        /// <param name="country">A country: an ISO 3166-1 alpha-2 country code. Provide this parameter to ensure that the category exists for a particular country.</param>
        /// <param name="limit">The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50. </param>
        /// <param name="offset">The index of the first item to return. Default: 0 (the first object). Use with <paramref name="limit"/> to get the next set of items.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<SimplifiedPlaylistsPageResponse> GetAsync(string country = null, int? limit = null, int? offset = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}
