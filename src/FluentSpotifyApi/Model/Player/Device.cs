using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.Model.Player
{
    /// <summary>
    /// The device.
    /// </summary>
    public class Device : JsonObject
    {
        /// <summary>
        /// The device ID. This may be <c>null</c>.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// If this device is the currently active device.
        /// </summary>
        [JsonPropertyName("is_active")]
        public bool IsActive { get; set; }

        /// <summary>
        /// If this device is currently in a private session.
        /// </summary>
        [JsonPropertyName("is_private_session")]
        public bool IsPrivateSession { get; set; }

        /// <summary>
        /// Whether controlling this device is restricted. At present if this is <c>true</c> then no Web API commands will be accepted by this device.
        /// </summary>
        [JsonPropertyName("is_restricted")]
        public bool IsRestricted { get; set; }

        /// <summary>
        /// The name of the device.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Device type, such as “computer”, “smartphone” or “speaker”.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// The current volume in percent. This may be <c>null</c>.
        /// </summary>
        [JsonPropertyName("volume_percent")]
        public int? VolumePercent { get; set; }
    }
}
