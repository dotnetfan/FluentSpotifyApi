using Newtonsoft.Json;

namespace FluentSpotifyApi.Model
{
    /// <summary>
    /// The playlist track.
    /// </summary>
    public class PlaylistTrack
    {
        /// <summary>
        /// Gets or sets the added at.
        /// </summary>
        /// <value>
        /// The added at.
        /// </value>
        [JsonProperty(PropertyName = "added_at")]
        public string AddedAt { get; set; }

        /// <summary>
        /// Gets or sets the added by.
        /// </summary>
        /// <value>
        /// The added by.
        /// </value>
        [JsonProperty(PropertyName = "added_by")]
        public PublicUser AddedBy { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is local.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is local; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty(PropertyName = "is_local")]
        public bool IsLocal { get; set; }

        /// <summary>
        /// Gets or sets the track.
        /// </summary>
        /// <value>
        /// The track.
        /// </value>
        [JsonProperty(PropertyName = "track")]
        public FullTrack Track { get; set; }
    }
}
