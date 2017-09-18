namespace FluentSpotifyApi.Builder.Search
{
    /// <summary>
    /// The builder for "search?type={entity}" endpoint.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISearchEntityBuilder<T>
    {
        /// <summary>
        /// Gets builder for "search?type={entity}&amp;q={query}" endpoint.
        /// </summary>
        /// <param name="query">The query.</param>
        ISearchEntityQueryBuilder<T> Matching(string query);
    }
}
