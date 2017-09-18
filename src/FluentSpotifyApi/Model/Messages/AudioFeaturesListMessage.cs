using FluentSpotifyApi.Model.Audio;
using Newtonsoft.Json;

namespace FluentSpotifyApi.Model.Messages
{
    /// <summary>
    /// The audio features list message.
    /// </summary>
    public class AudioFeaturesListMessage
    {
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        [JsonProperty(PropertyName = "audio_features")]
        public AudioFeatures[] Items { get; set; }
    }
}
