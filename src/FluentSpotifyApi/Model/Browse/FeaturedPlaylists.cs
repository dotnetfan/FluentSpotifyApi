using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;
using FluentSpotifyApi.Model.Playlists;

namespace FluentSpotifyApi.Model.Browse
{
    /// <summary>
    /// The featured playlists.
    /// </summary>
    public class FeaturedPlaylists : JsonObject
    {
        /// <summary>
        /// The message.
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        /// <summary>
        /// The playlists page.
        /// </summary>
        [JsonPropertyName("playlists")]
        public Page<SimplifiedPlaylist> Playlists { get; set; }
    }
}
