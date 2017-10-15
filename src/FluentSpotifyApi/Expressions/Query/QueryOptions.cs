namespace FluentSpotifyApi.Expressions.Query
{
    /// <summary>
    /// The query options.
    /// </summary>
    public class QueryOptions
    {
        /// <summary>
        /// If set to <c>true</c>, removes ["] characters from exact match and [":] characters from partial match.
        /// Set to <c>false</c> by default.
        /// </summary>
        public bool RemoveSpecialCharacters { get; set; }

        /// <summary>
        /// If set to <c>true</c>, converts OR and NOT words to lowercase and distributes field name and NOT operator
        /// in partial match. (e.g. <c>f => f.Artist.Contains("NOT") &amp;&amp; !f.Artist.Contains("nice so*")</c> 
        /// will be converted into "artist:not NOT artist:nice NOT artist:so*")
        /// Set to <c>false</c> by default.
        /// </summary>
        public bool NormalizePartialMatch { get; set; }
    }
}
