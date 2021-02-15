using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.Model
{
    /// <summary>
    /// The cursor based page.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CursorBasedPage<T> : JsonObject
    {
        /// <summary>
        /// The cursors used to find the next set of items.
        /// </summary>
        [JsonPropertyName("cursors")]
        public Cursor Cursors { get; set; }

        /// <summary>
        /// A link to the Web API endpoint returning the full result of the request.
        /// </summary>
        [JsonPropertyName("href")]
        public string Href { get; set; }

        /// <summary>
        /// The requested data.
        /// </summary>
        [JsonPropertyName("items")]
        public T[] Items { get; set; }

        /// <summary>
        /// The maximum number of items in the response (as set in the query or by default).
        /// </summary>
        [JsonPropertyName("limit")]
        public int Limit { get; set; }

        /// <summary>
        /// URL to the next page of items. (<c>null</c> if none).
        /// </summary>
        [JsonPropertyName("next")]
        public string Next { get; set; }

        /// <summary>
        /// The total number of items available to return.
        /// </summary>
        [JsonPropertyName("total")]
        public int Total { get; set; }
    }
}
