using System.Collections.Generic;
using FluentSpotifyApi.Core.Model;
using Newtonsoft.Json;

namespace FluentSpotifyApi.Model
{
    /// <summary>
    /// The base class for playlists.
    /// </summary>
    public abstract class PlaylistBase
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="PlaylistBase"/> is collaborative.
        /// </summary>
        /// <value>
        ///   <c>true</c> if collaborative; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty(PropertyName = "collaborative")]
        public bool Collaborative { get; set; }

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
        /// Gets or sets the owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        [JsonProperty(PropertyName = "owner")]
        public PublicUser Owner { get; set; }

        /// <summary>
        /// Gets or sets the public.
        /// </summary>
        /// <value>
        /// The public.
        /// </value>
        [JsonProperty(PropertyName = "public")]
        public bool? Public { get; set; }

        /// <summary>
        /// Gets or sets the snapshot identifier.
        /// </summary>
        /// <value>
        /// The snapshot identifier.
        /// </value>
        [JsonProperty(PropertyName = "snapshot_id")]
        public string SnapshotId { get; set; }

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
