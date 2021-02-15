using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.Model.Albums
{
    /// <summary>
    /// The albums response.
    /// </summary>
    public class AlbumsResponse : JsonObject
    {
        /// <summary>
        /// The albums.
        /// </summary>
        [JsonPropertyName("albums")]
        public Album[] Items { get; set; }
    }
}
