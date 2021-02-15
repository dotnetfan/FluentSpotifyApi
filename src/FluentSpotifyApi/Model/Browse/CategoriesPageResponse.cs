using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.Model.Browse
{
    /// <summary>
    /// The categories page response.
    /// </summary>
    public class CategoriesPageResponse : JsonObject
    {
        /// <summary>
        /// The categories page.
        /// </summary>
        [JsonPropertyName("categories")]
        public Page<Category> Page { get; set; }
    }
}
