using System.Collections.Generic;
using Newtonsoft.Json;

namespace FluentSpotifyApi.Model
{
    /// <summary>
    /// The full track.
    /// </summary>
    /// <seealso cref="FluentSpotifyApi.Model.TrackBase" />
    public class FullTrack : TrackBase
    {
        /// <summary>
        /// Gets or sets the album.
        /// </summary>
        /// <value>
        /// The album.
        /// </value>
        [JsonProperty(PropertyName = "album")]
        public SimpleAlbum Album { get; set; }

        /// <summary>
        /// Gets or sets the external ids.
        /// </summary>
        /// <value>
        /// The external ids.
        /// </value>
        [JsonProperty(PropertyName = "external_ids")]
        public IDictionary<string, string> ExternalIds { get; set; }

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
