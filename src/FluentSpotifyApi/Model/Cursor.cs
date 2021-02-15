using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.Model
{
    /// <summary>
    /// The cursor.
    /// </summary>
    public class Cursor : JsonObject
    {
        /// <summary>
        /// The cursor to use as key to find the next page of items.
        /// </summary>
        [JsonPropertyName("after")]
        public string After { get; set; }
    }
}
