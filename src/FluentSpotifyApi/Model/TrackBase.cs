using System.Collections.Generic;
using Newtonsoft.Json;

namespace FluentSpotifyApi.Model
{
    /// <summary>
    /// The base class for tracks.
    /// </summary>
    public class TrackBase
    {
        /// <summary>
        /// Gets or sets the artists.
        /// </summary>
        /// <value>
        /// The artists.
        /// </value>
        [JsonProperty(PropertyName = "artists")]
        public SimpleArtist[] Artists { get; set; }

        /// <summary>
        /// Gets or sets the available markets.
        /// </summary>
        /// <value>
        /// The available markets.
        /// </value>
        [JsonProperty(PropertyName = "available_markets")]
        public List<string> AvailableMarkets { get; set; }

        /// <summary>
        /// Gets or sets the disc number.
        /// </summary>
        /// <value>
        /// The disc number.
        /// </value>
        [JsonProperty(PropertyName = "disc_number")]
        public int DiscNumber { get; set; }

        /// <summary>
        /// Gets or sets the duration ms.
        /// </summary>
        /// <value>
        /// The duration ms.
        /// </value>
        [JsonProperty(PropertyName = "duration_ms")]
        public int DurationMs { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is explicit.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is explicit; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty(PropertyName = "explicit")]
        public bool IsExplicit { get; set; }

        /// <summary>
        /// Gets or sets the external urls.
        /// </summary>
        /// <value>
        /// The external urls.
        /// </value>
        [JsonProperty(PropertyName = "external_urls")]
        public IDictionary<string, string> ExternalUrls { get; set; }

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
        /// Gets or sets a value indicating whether this instance is playable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is playable; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty(PropertyName = "is_playable")]
        public bool IsPlayable { get; set; }

        /// <summary>
        /// Gets or sets the linked from.
        /// </summary>
        /// <value>
        /// The linked from.
        /// </value>
        [JsonProperty(PropertyName = "linked_from")]
        public TrackLink LinkedFrom { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the preview URL.
        /// </summary>
        /// <value>
        /// The preview URL.
        /// </value>
        [JsonProperty(PropertyName = "preview_url")]
        public string PreviewUrl { get; set; }

        /// <summary>
        /// Gets or sets the track number.
        /// </summary>
        /// <value>
        /// The track number.
        /// </value>
        [JsonProperty(PropertyName = "track_number")]
        public int TrackNumber { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the URI.
        /// </summary>
        /// <value>
        /// The URI.
        /// </value>
        [JsonProperty(PropertyName = "uri")]
        public string Uri { get; set; }
    }
}
