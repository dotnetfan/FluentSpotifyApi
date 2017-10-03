namespace FluentSpotifyApi.Builder.Search
{
    /// <summary>
    /// The builder for "search?type={entities}" endpoint.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISearchTypeBuilder<T>
    {
        /// <summary>
        /// Gets builder for "search?type={entities}&amp;q={query}" endpoint.
        /// </summary>
        /// <param name="query">The query.</param>
        ISearchTypeQueryBuilder<T> Matching(string query);
    }
}
