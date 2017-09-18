using Newtonsoft.Json;

namespace FluentSpotifyApi.Builder.User.Playlists
{
    /// <summary>
    /// The DTO for updating the existing playlists.
    /// </summary>
    public sealed class UpdatePlaylistDto
    {
        /// <summary>
        /// Gets or sets the new name for the playlist.
        /// </summary>
        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value that indicates whether updated playlist will be public.
        /// </summary>
        [JsonProperty(PropertyName = "public", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Public { get; set; }

        /// <summary>
        /// Gets or sets value that indicates whether updated playlist will be collaborative.
        /// </summary>
        [JsonProperty(PropertyName = "collaborative", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Collaborative { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [JsonProperty(PropertyName = "description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
    }
}
