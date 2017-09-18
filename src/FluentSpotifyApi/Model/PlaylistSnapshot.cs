using Newtonsoft.Json;

namespace FluentSpotifyApi.Model
{
    /// <summary>
    /// The playlist snapshot.
    /// </summary>
    public class PlaylistSnapshot
    {
        /// <summary>
        /// Gets or sets the snapshot identifier.
        /// </summary>
        /// <value>
        /// The snapshot identifier.
        /// </value>
        [JsonProperty(PropertyName = "snapshot_id")]
        public string SnapshotId { get; set; }
    }
}
