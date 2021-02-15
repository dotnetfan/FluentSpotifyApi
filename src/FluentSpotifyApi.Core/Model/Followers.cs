using System.Text.Json.Serialization;

namespace FluentSpotifyApi.Core.Model
{
    /// <summary>
    /// The followers.
    /// </summary>
    public class Followers : JsonObject
    {
        /// <summary>
        /// A link to the Web API endpoint providing full details of the followers; <c>null</c> if not available.
        /// Please note that this will always be set to <c>null</c>, as the Web API does not support it at the moment.
        /// </summary>
        [JsonPropertyName("href")]
        public string Href { get; set; }

        /// <summary>
        /// The total number of followers.
        /// </summary>
        [JsonPropertyName("total")]
        public int Total { get; set; }
    }
}
