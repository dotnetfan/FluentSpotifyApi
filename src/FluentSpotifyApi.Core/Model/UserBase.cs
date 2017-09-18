using System.Collections.Generic;
using Newtonsoft.Json;

namespace FluentSpotifyApi.Core.Model
{
    /// <summary>
    /// The base class for users.
    /// </summary>
    /// <seealso cref="IUser" />
    public abstract class UserBase : IUser
    {
        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        [JsonProperty(PropertyName = "display_name")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the external urls.
        /// </summary>
        /// <value>
        /// The external urls.
        /// </value>
        [JsonProperty(PropertyName = "external_urls")]
        public IDictionary<string, string> ExternalUrls { get; set; }

        /// <summary>
        /// Gets or sets the followers.
        /// </summary>
        /// <value>
        /// The followers.
        /// </value>
        [JsonProperty(PropertyName = "followers")]
        public Followers Followers { get; set; }

        /// <summary>
        /// Gets or sets the href.
        /// </summary>
        /// <value>
        /// The href.
        /// </value>
        [JsonProperty(PropertyName = "href")]
        public string Href { get; set; }

        /// <summary>
        /// Gets the identifier.
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
