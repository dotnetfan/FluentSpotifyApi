using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;
using FluentSpotifyApi.Model.Albums;

namespace FluentSpotifyApi.Model.Browse
{
    /// <summary>
    /// The new releases.
    /// </summary>
    public class NewReleases : JsonObject
    {
        /// <summary>
        /// The message.
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        /// <summary>
        /// The albums page.
        /// </summary>
        [JsonPropertyName("albums")]
        public Page<SimplifiedAlbum> Albums { get; set; }
    }
}
