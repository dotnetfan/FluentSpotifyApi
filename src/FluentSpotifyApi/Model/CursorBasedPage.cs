using Newtonsoft.Json;

namespace FluentSpotifyApi.Model
{
    /// <summary>
    /// The cursor based page.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CursorBasedPage<T>
    {
        /// <summary>
        /// Gets or sets the href.
        /// </summary>
        /// <value>
        /// The href.
        /// </value>
        [JsonProperty(PropertyName = "href")]
        public string Href { get; set; }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        [JsonProperty(PropertyName = "items")]
        public T[] Items { get; set; }

        /// <summary>
        /// Gets or sets the limit.
        /// </summary>
        /// <value>
        /// The limit.
        /// </value>
        [JsonProperty(PropertyName = "limit")]
        public int Limit { get; set; }

        /// <summary>
        /// Gets or sets the next.
        /// </summary>
        /// <value>
        /// The next.
        /// </value>
        [JsonProperty(PropertyName = "next")]
        public string Next { get; set; }

        /// <summary>
        /// Gets or sets the cursors.
        /// </summary>
        /// <value>
        /// The cursors.
        /// </value>
        [JsonProperty(PropertyName = "cursors")]
        public Cursor Cursors { get; set; }

        /// <summary>
        /// Gets or sets the total.
        /// </summary>
        /// <value>
        /// The total.
        /// </value>
        [JsonProperty(PropertyName = "total")]
        public int Total { get; set; }
    }
}
