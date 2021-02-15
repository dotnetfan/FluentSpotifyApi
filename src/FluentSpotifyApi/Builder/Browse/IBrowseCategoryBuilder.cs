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
        IBrowseCategoryPlaylistsBuilder Playlists { get; }

        /// <summary>
        /// Gets a single category used to tag items in Spotify (on, for example, the Spotify player’s “Browse” tab).
        /// </summary>
        /// <param name="country">A country: an ISO 3166-1 alpha-2 country code. Provide this parameter to ensure that the category exists for a particular country.</param>
        /// <param name="locale">
        /// The desired language, consisting of an ISO 639 language code and an ISO 3166-1 alpha-2 country code, joined by an underscore. (e.g. es_MX).
        /// Provide this parameter if you want the category metadata returned in a particular language.
        /// Note that, if locale is not supplied, or if the specified language is not available, all strings will be returned in the Spotify default language (American English).
        /// </param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<Category> GetAsync(string country = null, string locale = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}
