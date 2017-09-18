namespace FluentSpotifyApi.Builder.Search
{
    /// <summary>
    /// The builder for "search?type={entities}" endpoint.
    /// </summary>
    public interface ISearchEntitiesBuilder
    {
        /// <summary>
        /// Gets builder for "search?type={entities}&amp;q={query}" endpoint.
        /// </summary>
        /// <param name="query">The query.</param>
        ISearchEntitiesQueryBuilder Matching(string query);
    }
}
