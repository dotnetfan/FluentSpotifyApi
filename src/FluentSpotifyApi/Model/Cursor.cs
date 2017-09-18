using Newtonsoft.Json;

namespace FluentSpotifyApi.Model
{
    /// <summary>
    /// The cursor.
    /// </summary>
    public class Cursor
    {
        /// <summary>
        /// Gets or sets the before.
        /// </summary>
        /// <value>
        /// The before.
        /// </value>
        [JsonProperty(PropertyName = "before")]
        public string Before { get; set; }

        /// <summary>
        /// Gets or sets the after.
        /// </summary>
        /// <value>
        /// The after.
        /// </value>
        [JsonProperty(PropertyName = "after")]
        public string After { get; set; }
    }
}
