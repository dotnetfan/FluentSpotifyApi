using FluentSpotifyApi.Core.Model;
using Newtonsoft.Json;

namespace FluentSpotifyApi.Model.Browse
{
    /// <summary>
    /// The category.
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Gets or sets the href.
        /// </summary>
        /// <value>
        /// The href.
        /// </value>
        [JsonProperty(PropertyName = "href")]
        public string Href { get; set; }

        /// <summary>
        /// Gets or sets the icons.
        /// </summary>
        /// <value>
        /// The icons.
        /// </value>
        [JsonProperty(PropertyName = "icons")]
        public Image[] Icons { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}
