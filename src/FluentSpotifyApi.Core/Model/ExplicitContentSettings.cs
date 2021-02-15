using System.Text.Json.Serialization;

namespace FluentSpotifyApi.Core.Model
{
    /// <summary>
    /// Explicit content settings.
    /// </summary>
    public class ExplicitContentSettings : JsonObject
    {
        /// <summary>
        /// When <c>true</c>, indicates that explicit content should not be played.
        /// </summary>
        [JsonPropertyName("filter_enabled")]
        public bool FilterEnabled { get; set; }

        /// <summary>
        /// When <c>true</c>, indicates that the explicit content setting is locked and can’t be changed by the user.
        /// </summary>
        [JsonPropertyName("filter_locked")]
        public bool FilterLocked { get; set; }
    }
}
