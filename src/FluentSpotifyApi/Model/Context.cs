using System.Collections.Generic;
using Newtonsoft.Json;

namespace FluentSpotifyApi.Model
{
    /// <summary>
    /// The context.
    /// </summary>
    public class Context
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the href.
        /// </summary>
        /// <value>
        /// The href.
        /// </value>
        [JsonProperty(PropertyName = "href")]
        public string Href { get; set; }

        /// <summary>
        /// Gets or sets the external urls.
        /// </summary>
        /// <value>
        /// The external urls.
        /// </value>
        [JsonProperty(PropertyName = "external_urls")]
        public IDictionary<string, string> ExternalUrls { get; set; }

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
