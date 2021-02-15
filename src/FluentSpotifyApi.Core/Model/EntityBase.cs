using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FluentSpotifyApi.Core.Model
{
    /// <summary>
    /// The base class for Spotify entities.
    /// </summary>
    public abstract class EntityBase : JsonObject
    {
        /// <summary>
        /// The entity type.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// The Spotify ID for the entity.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The Spotify URI for the entity.
        /// </summary>
        [JsonPropertyName("uri")]
        public string Uri { get; set; }

        /// <summary>
        /// A link to the Web API endpoint providing full details of the entity.
        /// </summary>
        [JsonPropertyName("href")]
        public string Href { get; set; }

        /// <summary>
        /// Known external URLs for the entity.
        /// </summary>
        [JsonPropertyName("external_urls")]
        public IDictionary<string, string> ExternalUrls { get; set; }
    }
}
