using System.Text.Json.Serialization;

namespace FluentSpotifyApi.Model.Player
{
    /// <summary>
    /// The currently playing context.
    /// </summary>
    /// <seealso cref="CurrentlyPlayingItem" />
    public class CurrentlyPlayingContext : CurrentlyPlayingItem
    {
        /// <summary>
        /// The device that is currently active.
        /// </summary>
        [JsonPropertyName("device")]
        public Device Device { get; set; }

        /// <summary>
        /// <c>off</c>, <c>track</c>, <c>context</c>.
        /// </summary>
        [JsonPropertyName("repeat_state")]
        public string RepeatState { get; set; }

        /// <summary>
        /// If shuffle is on or off.
        /// </summary>
        [JsonPropertyName("shuffle_state")]
        public bool ShuffleState { get; set; }
    }
}
