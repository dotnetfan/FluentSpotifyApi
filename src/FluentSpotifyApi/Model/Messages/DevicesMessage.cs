using Newtonsoft.Json;

namespace FluentSpotifyApi.Model.Messages
{
    /// <summary>
    /// The devices message.
    /// </summary>
    /// <seealso cref="FluentSpotifyApi.Model.PlayerBase" />
    public class DevicesMessage : PlayerBase
    {
        /// <summary>
        /// Gets or sets the devices.
        /// </summary>
        /// <value>
        /// The devices.
        /// </value>
        [JsonProperty(PropertyName = "devices")]
        public Device[] Devices { get; set; }
    }
}
