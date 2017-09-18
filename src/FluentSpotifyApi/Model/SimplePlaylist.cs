using Newtonsoft.Json;

namespace FluentSpotifyApi.Model
{
    /// <summary>
    /// The simple playlist.
    /// </summary>
    /// <seealso cref="FluentSpotifyApi.Model.PlaylistBase" />
    public class SimplePlaylist : PlaylistBase
    {
        /// <summary>
        /// Gets or sets the tracks.
        /// </summary>
        /// <value>
        /// The tracks.
        /// </value>
        [JsonProperty(PropertyName = "tracks")]
        public TracksLink Tracks { get; set; }
    }
}
