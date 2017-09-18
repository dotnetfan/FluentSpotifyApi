using System.Collections.Generic;
using Newtonsoft.Json;

namespace FluentSpotifyApi.Model
{
    /// <summary>
    /// The full album.
    /// </summary>
    /// <seealso cref="FluentSpotifyApi.Model.AlbumBase" />
    public class FullAlbum : AlbumBase
    {
        /// <summary>
        /// Gets or sets the copyrights.
        /// </summary>
        /// <value>
        /// The copyrights.
        /// </value>
        [JsonProperty(PropertyName = "copyrights")]
        public Copyright[] Copyrights { get; set; }

        /// <summary>
        /// Gets or sets the external ids.
        /// </summary>
        /// <value>
        /// The external ids.
        /// </value>
        [JsonProperty(PropertyName = "external_ids")]
        public IDictionary<string, string> ExternalIds { get; set; }

        /// <summary>
        /// Gets or sets the genres.
        /// </summary>
        /// <value>
        /// The genres.
        /// </value>
        [JsonProperty(PropertyName = "genres")]
        public string[] Genres { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the popularity.
        /// </summary>
        /// <value>
        /// The popularity.
        /// </value>
        [JsonProperty(PropertyName = "popularity")]
        public int Popularity { get; set; }

        /// <summary>
        /// Gets or sets the release date.
        /// </summary>
        /// <value>
        /// The release date.
        /// </value>
        [JsonProperty(PropertyName = "release_date")]
        public string ReleaseDate { get; set; }

        /// <summary>
        /// Gets or sets the release date precision.
        /// </summary>
        /// <value>
        /// The release date precision.
        /// </value>
        [JsonProperty(PropertyName = "release_date_precision")]
        public string ReleaseDatePrecision { get; set; }

        /// <summary>
        /// Gets or sets the tracks.
        /// </summary>
        /// <value>
        /// The tracks.
        /// </value>
        [JsonProperty(PropertyName = "tracks")]
        public Page<SimpleTrack> Tracks { get; set; }
    }
}
