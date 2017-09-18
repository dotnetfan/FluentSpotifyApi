using Newtonsoft.Json;

namespace FluentSpotifyApi.Model
{
    /// <summary>
    /// The saved track.
    /// </summary>
    public class SavedTrack
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
        /// Gets or sets the track.
        /// </summary>
        /// <value>
        /// The track.
        /// </value>
        [JsonProperty(PropertyName = "track")]
        public FullTrack Track { get; set; }
    }
}
