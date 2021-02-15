using System.Collections.Generic;
using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.Model.Tracks
{
    /// <summary>
    /// The linked track.
    /// </summary>
    public class LinkedTrack : JsonObject
    {
        /// <summary>
        /// The object type: “track”.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// The Spotify ID for the track.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The Spotify URI for the track.
        /// </summary>
        [JsonPropertyName("uri")]
        public string Uri { get; set; }

        /// <summary>
        /// A link to the Web API endpoint providing full details of the track.
        /// </summary>
        [JsonPropertyName("href")]
        public string Href { get; set; }

        /// <summary>
        /// Known external URLs for this track.
        /// </summary>
        [JsonPropertyName("external_urls")]
        public IDictionary<string, string> ExternalUrls { get; set; }
    }
}
