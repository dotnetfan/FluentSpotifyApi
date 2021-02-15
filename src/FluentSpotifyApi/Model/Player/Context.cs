using System.Collections.Generic;
using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.Model.Player
{
    /// <summary>
    /// The play context.
    /// </summary>
    public class Context : JsonObject
    {
        /// <summary>
        /// External URLs for this context.
        /// </summary>
        [JsonPropertyName("external_urls")]
        public IDictionary<string, string> ExternalUrls { get; set; }

        /// <summary>
        /// A link to the Web API endpoint providing full details of the track.
        /// </summary>
        [JsonPropertyName("href")]
        public string Href { get; set; }

        /// <summary>
        /// The object type, e.g. “artist”, “playlist”, “album”, “show”.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// The Spotify URI for the context.
        /// </summary>
        [JsonPropertyName("uri")]
        public string Uri { get; set; }
    }
}
