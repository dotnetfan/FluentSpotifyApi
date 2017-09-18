using FluentSpotifyApi.Core.Model;
using Newtonsoft.Json;

namespace FluentSpotifyApi.Model
{
    /// <summary>
    /// The full artist.
    /// </summary>
    /// <seealso cref="FluentSpotifyApi.Model.ArtistBase" />
    public class FullArtist : ArtistBase
    {
        /// <summary>
        /// Gets or sets the followers.
        /// </summary>
        /// <value>
        /// The followers.
        /// </value>
        [JsonProperty(PropertyName = "followers")]
        public Followers Followers { get; set; }

        /// <summary>
        /// Gets or sets the genres.
        /// </summary>
        /// <value>
        /// The genres.
        /// </value>
        [JsonProperty(PropertyName = "genres")]
        public string[] Genres { get; set; }

        /// <summary>
        /// Gets or sets the images.
        /// </summary>
        /// <value>
        /// The images.
        /// </value>
        [JsonProperty(PropertyName = "images")]
        public Image[] Images { get; set; }

        /// <summary>
        /// Gets or sets the popularity.
        /// </summary>
        /// <value>
        /// The popularity.
        /// </value>
        [JsonProperty(PropertyName = "popularity")]
        public int Popularity { get; set; }
    }
}
