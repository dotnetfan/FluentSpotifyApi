using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.Model.Player
{
    /// <summary>
    /// The devices response.
    /// </summary>
    public class DevicesResponse : JsonObject
    {
        /// <summary>
        /// The devices.
        /// </summary>
        [JsonPropertyName("devices")]
        public Device[] Items { get; set; }
    }
}
