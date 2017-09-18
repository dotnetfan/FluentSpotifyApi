using Newtonsoft.Json;

namespace FluentSpotifyApi.Model
{
    /// <summary>
    /// The followed artists
    /// </summary>
    public class FollowedArtists
    {
        /// <summary>
        /// Gets or sets the page.
        /// </summary>
        /// <value>
        /// The page.
        /// </value>
        [JsonProperty(PropertyName = "artists")]
        public CursorBasedPage<FullArtist> Page { get; set; }
    }
}
