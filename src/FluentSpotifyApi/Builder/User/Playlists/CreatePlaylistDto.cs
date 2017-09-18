using Newtonsoft.Json;

namespace FluentSpotifyApi.Builder.User.Playlists
{
    /// <summary>
    /// The DTO for creating new playlists.
    /// </summary>
    public sealed class CreatePlaylistDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePlaylistDto"/> class.
        /// </summary>
        public CreatePlaylistDto()
        {
            this.Public = true;
        }

        /// <summary>
        /// Gets or sets the name for the new playlist.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets value that indicates whether new playlist will be public. Default: true.
        /// </summary>
        [JsonProperty(PropertyName = "public")]
        public bool Public { get; set; }

        /// <summary>
        /// Gets or sets value that indicates whether new playlist will be collaborative. Default: false.
        /// </summary>
        [JsonProperty(PropertyName = "collaborative")]
        public bool Collaborative { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [JsonProperty(PropertyName = "description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
    }
}
