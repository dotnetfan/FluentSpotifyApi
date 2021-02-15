using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FluentSpotifyApi.Builder.Search
{
    /// <summary>
    /// The builder for "search?type={entities}&amp;q={query}" endpoint.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISearchTypeQueryBuilder<T>
    {
        /// <summary>
        /// Searches for the entities of specified type by specific query.
        /// </summary>
        /// <param name="market">
        /// An ISO 3166-1 alpha-2 country code or the string <c>from_token</c>. If a country code is specified, only content that is playable in that market is returned.
        /// Playlist results are not affected by the market parameter.
        /// If market is set to <c>from_token</c>, and a valid access token is specified in the request header, only content playable in the country associated with the user account, is returned.
        /// Users can view the country that is associated with their account in the account settings.
        /// A user must grant access to the <c>user-read-private</c> scope prior to when the access token is issued.
        /// </param>
        /// <param name="limit">
        /// The maximum number of results to return. Default: 20. Minimum: 1. Maximum: 50.
        /// Note: The limit is applied within each type, not on the total response.
        /// </param>
        /// <param name="offset">
        /// The index of the first result to return.
        /// Default: 0 (i.e., the first result). Maximum offset (including limit): 2,000. Use with <paramref name="limit"/> to get the next page of search results.
        /// </param>
        /// <param name="includeExternal">
        /// If <see cref="ExternalContent.Audio"/> is specified the response will include any relevant audio content that is hosted externally.
        /// By default external content is filtered out from responses.
        /// </param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<T> GetAsync(string market = null, int? limit = null, int? offset = null, IEnumerable<ExternalContent> includeExternal = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}
