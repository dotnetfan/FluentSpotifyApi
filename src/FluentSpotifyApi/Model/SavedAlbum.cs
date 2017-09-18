using Newtonsoft.Json;

namespace FluentSpotifyApi.Model
{
    /// <summary>
    /// The saved album.
    /// </summary>
    public class SavedAlbum
    {
        /// <summary>
        /// Gets or sets the added at.
        /// </summary>
        /// <value>
        /// The added at.
        /// </value>
        [JsonProperty(PropertyName = "added_at")]
        public string AddedAt { get; set; }

        /// <summary>
        /// Gets or sets the album.
        /// </summary>
        /// <value>
        /// The album.
        /// </value>
        [JsonProperty(PropertyName = "album")]
        public FullAlbum Album { get; set; }
    }
}
