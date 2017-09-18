using Newtonsoft.Json;

namespace FluentSpotifyApi.Model
{
    /// <summary>
    /// The play history.
    /// </summary>
    public class PlayHistory
    {
        /// <summary>
        /// Gets or sets the track.
        /// </summary>
        /// <value>
        /// The track.
        /// </value>
        [JsonProperty(PropertyName = "track")]
        public SimpleTrack Track { get; set; }

        /// <summary>
        /// Gets or sets the played at.
        /// </summary>
        /// <value>
        /// The played at.
        /// </value>
        [JsonProperty(PropertyName = "played_at")]
        public string PlayedAt { get; set; }

        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        [JsonProperty(PropertyName = "context")]
        public Context Context { get; set; }
    }
}
