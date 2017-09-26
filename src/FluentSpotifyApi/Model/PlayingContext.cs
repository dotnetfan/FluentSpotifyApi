using Newtonsoft.Json;

namespace FluentSpotifyApi.Model
{
    /// <summary>
    /// The playing context.
    /// </summary>
    /// <seealso cref="FluentSpotifyApi.Model.PlayerBase" />
    public class PlayingContext : PlayingObject
    {
        /// <summary>
        /// Gets or sets the device.
        /// </summary>
        /// <value>
        /// The device.
        /// </value>
        [JsonProperty(PropertyName = "device")]
        public Device Device { get; set; }

        /// <summary>
        /// Gets or sets the state of the repeat.
        /// </summary>
        /// <value>
        /// The state of the repeat.
        /// </value>
        [JsonProperty(PropertyName = "repeat_state")]
        public string RepeatState { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [shuffle state].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [shuffle state]; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty(PropertyName = "shuffle_state")]
        public bool ShuffleState { get; set; }
    }
}
