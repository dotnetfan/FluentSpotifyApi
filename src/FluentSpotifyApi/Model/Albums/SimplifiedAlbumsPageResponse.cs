using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.Model.Albums
{
    /// <summary>
    /// The simplified albums page response.
    /// </summary>
    public class SimplifiedAlbumsPageResponse : JsonObject
    {
        /// <summary>
        /// The simplified albums page.
        /// </summary>
        [JsonPropertyName("albums")]
        public Page<SimplifiedAlbum> Page { get; set; }
    }
}
