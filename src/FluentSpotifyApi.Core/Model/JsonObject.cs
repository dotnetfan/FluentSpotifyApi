using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FluentSpotifyApi.Core.Model
{
    /// <summary>
    /// The base class for all Spotify JSON objects.
    /// </summary>
    public abstract class JsonObject
    {
        /// <summary>
        /// Dictionary of properties that do not have matching member.
        /// </summary>
        [JsonExtensionData]
        public IDictionary<string, object> AdditionalProperties { get; set; }
    }
}
