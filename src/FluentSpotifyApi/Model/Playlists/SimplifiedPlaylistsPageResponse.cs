using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.Model.Playlists
{
    /// <summary>
    /// The simplified playlists page response.
    /// </summary>
    public class SimplifiedPlaylistsPageResponse : JsonObject
    {
        /// <summary>
        /// The simplified playlists page.
        /// </summary>
        [JsonPropertyName("playlists")]
        public Page<SimplifiedPlaylist> Page { get; set; }
    }
}
