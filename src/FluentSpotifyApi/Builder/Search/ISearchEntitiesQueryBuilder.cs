using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model;

namespace FluentSpotifyApi.Builder.Search
{
    /// <summary>
    /// The builder for "search?type={entities}&amp;q={query}" endpoint.
    /// </summary>
    public interface ISearchEntitiesQueryBuilder
    {
        /// <summary>
        /// Searches for the entities by specific query.
        /// </summary>
        /// <param name="market">An ISO 3166-1 alpha-2 country code or the string from_token.</param>
        /// <param name="limit">The maximum number of results to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">The index of the first result to return. Default: 0 (i.e., the first result). Maximum offset: 100.000.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<SearchResult> GetAsync(string market = null, int limit = 20, int offset = 0, CancellationToken cancellationToken = default(CancellationToken));
    }
}
