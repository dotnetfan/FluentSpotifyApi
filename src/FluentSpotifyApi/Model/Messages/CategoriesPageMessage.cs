using FluentSpotifyApi.Model.Browse;
using Newtonsoft.Json;

namespace FluentSpotifyApi.Model.Messages
{
    /// <summary>
    /// The categories page message.
    /// </summary>
    public class CategoriesPageMessage
    {
        /// <summary>
        /// Gets or sets the page.
        /// </summary>
        /// <value>
        /// The page.
        /// </value>
        [JsonProperty(PropertyName = "categories")]
        public Page<Category> Page { get; set; }
    }
}
