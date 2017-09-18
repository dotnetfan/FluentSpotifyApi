using Newtonsoft.Json;

namespace FluentSpotifyApi.Core.Model
{
    /// <summary>
    /// The regular error payload.
    /// </summary>
    public class RegularError
    {
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        [JsonProperty(PropertyName = "status")]
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }
}
