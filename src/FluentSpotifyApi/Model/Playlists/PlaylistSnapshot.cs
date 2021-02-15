using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.Model.Playlists
{
    /// <summary>
    /// The playlist snapshot.
    /// </summary>
    public class PlaylistSnapshot : JsonObject
    {
        /// <summary>
        /// Identifies playlist version for future requests.
        /// </summary>
        [JsonPropertyName("snapshot_id")]
        public string SnapshotId { get; set; }
    }
}
