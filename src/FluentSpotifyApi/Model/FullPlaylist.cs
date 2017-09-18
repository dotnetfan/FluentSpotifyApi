using FluentSpotifyApi.Core.Model;
using Newtonsoft.Json;

namespace FluentSpotifyApi.Model
{
    /// <summary>
    /// The full playlist.
    /// </summary>
    /// <seealso cref="FluentSpotifyApi.Model.PlaylistBase" />
    public class FullPlaylist : PlaylistBase
    {
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the followers.
        /// </summary>
        /// <value>
        /// The followers.
        /// </value>
        [JsonProperty(PropertyName = "followers")]
        public Followers Followers { get; set; }

        /// <summary>
        /// Gets or sets the tracks.
        /// </summary>
        /// <value>
        /// The tracks.
        /// </value>
        [JsonProperty(PropertyName = "tracks")]
        public Page<PlaylistTrack> Tracks { get; set; }
    }
}
