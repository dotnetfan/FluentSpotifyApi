using Newtonsoft.Json;

namespace FluentSpotifyApi.Core.Model.Wrappers
{
    /// <summary>
    /// The regular error message.
    /// </summary>
    public class RegularErrorMessage
    {
        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        [JsonProperty(PropertyName = "error", Required = Required.Always)]
        public RegularError Error { get; set; }
    }
}
