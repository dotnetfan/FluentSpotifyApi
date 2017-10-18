using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Browse;

namespace FluentSpotifyApi.Builder.Browse
{
    /// <summary>
    /// The builder for "categories/{id}" endpoint.
    /// </summary>
    public interface IBrowseCategoryBuilder
    {
        /// <summary>
        /// Gets builder for "browse/categories/{category_id}/playlists" endpoint.
        /// </summary>
        /// <value>
        /// The builder for "browse/categories/{category_id}/playlists" endpoint.
        /// </value>
        IBrowseCategoryPlaylistsBuilder Playlists { get; }

        /// <summary>
        /// Get a single category used to tag items in Spotify (on, for example, the Spotify player’s “Browse” tab).
        /// </summary>
        /// <param name="country">A country: an ISO 3166-1 alpha-2 country code.</param>
        /// <param name="locale">
        /// The desired language, consisting of an ISO 639 language code and an ISO 3166-1 alpha-2 country code, 
        /// joined by an underscore. (e.g. es_MX)
        /// </param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<Category> GetAsync(string country = null, string locale = null, CancellationToken cancellationToken = default(CancellationToken));       
    }
}
