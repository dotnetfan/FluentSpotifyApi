using System.Collections.Generic;
using System.Text.Json.Serialization;
using FluentSpotifyApi.Model.Tracks;

namespace FluentSpotifyApi.Model.Albums
{
    /// <summary>
    /// The album.
    /// </summary>
    /// <seealso cref="AlbumBase" />
    public class Album : AlbumBase
    {
        /// <summary>
        /// The copyright statements of the album.
        /// </summary>
        [JsonPropertyName("copyrights")]
        public Copyright[] Copyrights { get; set; }

        /// <summary>
        /// Known external IDs for the album.
        /// </summary>
        [JsonPropertyName("external_ids")]
        public IDictionary<string, string> ExternalIds { get; set; }

        /// <summary>
        /// A list of the genres used to classify the album. For example: “Prog Rock” , “Post-Grunge”. (If not yet classified, the array is empty.)
        /// </summary>
        [JsonPropertyName("genres")]
        public string[] Genres { get; set; }

        /// <summary>
        /// The label for the album.
        /// </summary>
        [JsonPropertyName("label")]
        public string Label { get; set; }

        /// <summary>
        /// The popularity of the album. The value will be between 0 and 100, with 100 being the most popular.
        /// The popularity is calculated from the popularity of the album’s individual tracks.
        /// </summary>
        [JsonPropertyName("popularity")]
        public int Popularity { get; set; }

        /// <summary>
        /// The tracks of the album.
        /// </summary>
        [JsonPropertyName("tracks")]
        public Page<SimplifiedTrack> Tracks { get; set; }
    }
}
