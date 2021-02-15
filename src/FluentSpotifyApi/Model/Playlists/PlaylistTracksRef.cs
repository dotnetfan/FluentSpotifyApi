using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.Model.Playlists
{
    /// <summary>
    /// The playlist tracks references.
    /// </summary>
    public class PlaylistTracksRef : JsonObject
    {
        /// <summary>
        /// A link to the Web API endpoint where full details of the playlist’s tracks can be retrieved.
        /// </summary>
        [JsonPropertyName("href")]
        public string Href { get; set; }

        /// <summary>
        /// Number of tracks in the playlist.
        /// </summary>
        [JsonPropertyName("total")]
        public int Total { get; set; }
    }
}
