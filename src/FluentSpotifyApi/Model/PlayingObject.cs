using Newtonsoft.Json;

namespace FluentSpotifyApi.Model
{
    /// <summary>
    /// The playing object.
    /// </summary>
    /// <seealso cref="FluentSpotifyApi.Model.PlayerBase" />
    public class PlayingObject : PlayerBase
    {
        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        [JsonProperty(PropertyName = "context")]
        public Context Context { get; set; }

        /// <summary>
        /// Gets or sets the timestamp.
        /// </summary>
        /// <value>
        /// The timestamp.
        /// </value>
        [JsonProperty(PropertyName = "timestamp")]
        public long Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the progress ms.
        /// </summary>
        /// <value>
        /// The progress ms.
        /// </value>
        [JsonProperty(PropertyName = "progress_ms")]
        public int? ProgressMs { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is playing.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is playing; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty(PropertyName = "is_playing")]
        public bool IsPlaying { get; set; }

        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        /// <value>
        /// The item.
        /// </value>
        [JsonProperty(PropertyName = "item")]
        public FullTrack Item { get; set; }
    }
}
