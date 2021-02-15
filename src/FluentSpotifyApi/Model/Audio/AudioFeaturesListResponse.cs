using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.Model.Audio
{
    /// <summary>
    /// The audio features list response.
    /// </summary>
    public class AudioFeaturesListResponse : JsonObject
    {
        /// <summary>
        /// The audio features list.
        /// </summary>
        [JsonPropertyName("audio_features")]
        public AudioFeatures[] Items { get; set; }
    }
}
