using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.Model.Player
{
    /// <summary>
    /// The disallows.
    /// </summary>
    public class Disallows : JsonObject
    {
        /// <summary>
        /// Interrupting playback.
        /// </summary>
        [JsonPropertyName("interrupting_playback")]
        public bool? InterruptingPlayback { get; set; }

        /// <summary>
        /// Pausing.
        /// </summary>
        [JsonPropertyName("pausing")]
        public bool? Pausing { get; set; }

        /// <summary>
        /// Resuming.
        /// </summary>
        [JsonPropertyName("resuming")]
        public bool? Resuming { get; set; }

        /// <summary>
        /// Seeking playback location.
        /// </summary>
        [JsonPropertyName("seeking")]
        public bool? Seeking { get; set; }

        /// <summary>
        /// Skipping to the next context.
        /// </summary>
        [JsonPropertyName("skipping_next")]
        public bool? SkippingNext { get; set; }

        /// <summary>
        /// Skipping to the previous context.
        /// </summary>
        [JsonPropertyName("skipping_prev")]
        public bool? SkippingPrev { get; set; }

        /// <summary>
        /// Toggling repeat context flag.
        /// </summary>
        [JsonPropertyName("toggling_repeat_context")]
        public bool? TogglingRepeatContext { get; set; }

        /// <summary>
        /// Toggling repeat track flag.
        /// </summary>
        [JsonPropertyName("toggling_repeat_track")]
        public bool? TogglingRepeatTrack { get; set; }

        /// <summary>
        /// Toggling shuffle flag.
        /// </summary>
        [JsonPropertyName("toggling_shuffle")]
        public bool? TogglingShuffle { get; set; }

        /// <summary>
        /// Transferring playback between devices.
        /// </summary>
        [JsonPropertyName("transferring_playback")]
        public bool? TransferringPlayback { get; set; }
    }
}
