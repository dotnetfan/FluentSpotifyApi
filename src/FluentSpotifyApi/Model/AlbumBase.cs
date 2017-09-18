using System.Collections.Generic;
using FluentSpotifyApi.Core.Model;
using Newtonsoft.Json;

namespace FluentSpotifyApi.Model
{
    /// <summary>
    /// The base class for albums.
    /// </summary>
    public abstract class AlbumBase
    {
        /// <summary>
        /// Gets or sets the type of the album.
        /// </summary>
        /// <value>
        /// The type of the album.
        /// </value>
        [JsonProperty(PropertyName = "album_type")]
        public string AlbumType { get; set; }

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
        public string[] AvailableMarkets { get; set; }

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
        /// Gets or sets the images.
        /// </summary>
        /// <value>
        /// The images.
        /// </value>
        [JsonProperty(PropertyName = "images")]
        public Image[] Images { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

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
