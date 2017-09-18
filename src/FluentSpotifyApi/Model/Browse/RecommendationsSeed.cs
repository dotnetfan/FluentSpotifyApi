using Newtonsoft.Json;

namespace FluentSpotifyApi.Model.Browse
{
    /// <summary>
    /// The recommendations seed
    /// </summary>
    public class RecommendationsSeed
    {
        /// <summary>
        /// Gets or sets the size of the after filtering.
        /// </summary>
        /// <value>
        /// The size of the after filtering.
        /// </value>
        [JsonProperty(PropertyName = "afterFilteringSize")]
        public int AfterFilteringSize { get; set; }

        /// <summary>
        /// Gets or sets the size of the after relinking.
        /// </summary>
        /// <value>
        /// The size of the after relinking.
        /// </value>
        [JsonProperty(PropertyName = "afterRelinkingSize")]
        public int AfterRelinkingSize { get; set; }

        /// <summary>
        /// Gets or sets the href.
        /// </summary>
        /// <value>
        /// The href.
        /// </value>
        [JsonProperty(PropertyName = "href")]
        public string Href { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the initial size of the pool.
        /// </summary>
        /// <value>
        /// The initial size of the pool.
        /// </value>
        [JsonProperty(PropertyName = "initialPoolSize")]
        public int InitialPoolSize { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
    }
}
