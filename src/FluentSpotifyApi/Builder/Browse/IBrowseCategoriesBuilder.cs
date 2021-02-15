using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Browse;

namespace FluentSpotifyApi.Builder.Browse
{
    /// <summary>
    /// The builder for "browse/categories" endpoint.
    /// </summary>
    public interface IBrowseCategoriesBuilder
    {
        /// <summary>
        /// Gets a list of categories used to tag items in Spotify (on, for example, the Spotify player’s “Browse” tab).
        /// </summary>
        /// <param name="country">
        /// A country: an ISO 3166-1 alpha-2 country code.
        /// Provide this parameter if you want to narrow the list of returned categories to those relevant to a particular country.
        /// If omitted, the returned items will be globally relevant.
        /// </param>
        /// <param name="locale">
        /// The desired language, consisting of an ISO 639 language code and an ISO 3166-1 alpha-2 country code, joined by an underscore. (e.g. es_MX).
        /// Provide this parameter if you want the category metadata returned in a particular language.
        /// Note that, if locale is not supplied, or if the specified language is not available, all strings will be returned in the Spotify default language (American English).
        /// </param>
        /// <param name="limit">The maximum number of categories to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">The index of the first item to return. Default: 0 (the first object). Use with <paramref name="limit"/> to get the next set of categories.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<CategoriesPageResponse> GetAsync(string country = null, string locale = null, int? limit = null, int? offset = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}
