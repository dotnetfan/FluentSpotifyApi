using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Messages;

namespace FluentSpotifyApi.Builder.Browse
{
    /// <summary>
    /// The builder for "browse/categories" endpoint.
    /// </summary>
    public interface IBrowseCategoriesBuilder
    {
        /// <summary>
        /// Get a list of categories used to tag items in Spotify (on, for example, the Spotify player’s “Browse” tab).
        /// </summary>
        /// <param name="country">A country: an ISO 3166-1 alpha-2 country code.</param>
        /// <param name="locale">
        /// The desired language, consisting of an ISO 639 language code and an ISO 3166-1 alpha-2 country code, joined by an underscore. (e.g. es_MX)
        /// </param>
        /// <param name="limit">The maximum number of categories to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">The index of the first item to return. Default: 0 (the first object).</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<CategoriesPageMessage> GetAsync(string country = null, string locale = null, int limit = 20, int offset = 0, CancellationToken cancellationToken = default(CancellationToken));
    }
}
