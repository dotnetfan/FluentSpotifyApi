using Newtonsoft.Json;

namespace FluentSpotifyApi.Model
{
    /// <summary>
    /// The search result.
    /// </summary>
    public class SearchResult
    {
        /// <summary>
        /// Gets or sets the albums.
        /// </summary>
        /// <value>
        /// The albums.
        /// </value>
        [JsonProperty(PropertyName = "albums")]
        public Page<SimpleAlbum> Albums { get; set; }

        /// <summary>
        /// Gets or sets the artists.
        /// </summary>
        /// <value>
        /// The artists.
        /// </value>
        [JsonProperty(PropertyName = "artists")]
        public Page<FullArtist> Artists { get; set; }

        /// <summary>
        /// Gets or sets the tracks.
        /// </summary>
        /// <value>
        /// The tracks.
        /// </value>
        [JsonProperty(PropertyName = "tracks")]
        public Page<FullTrack> Tracks { get; set; }

        /// <summary>
        /// Gets or sets the playlists.
        /// </summary>
        /// <value>
        /// The playlists.
        /// </value>
        [JsonProperty(PropertyName = "playlists")]
        public Page<SimplePlaylist> Playlists { get; set; }
    }
}
