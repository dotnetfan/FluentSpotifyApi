using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.Model.Artists
{
    /// <summary>
    /// The artist.
    /// </summary>
    /// <seealso cref="ArtistBase" />
    public class Artist : ArtistBase
    {
        /// <summary>
        /// Information about the followers of the artist.
        /// </summary>
        [JsonPropertyName("followers")]
        public Followers Followers { get; set; }

        /// <summary>
        /// A list of the genres the artist is associated with. For example: <c>"Prog Rock"</c>, <c>"Post-Grunge"</c>. (If not yet classified, the array is empty.)
        /// </summary>
        [JsonPropertyName("genres")]
        public string[] Genres { get; set; }

        /// <summary>
        /// Images of the artist in various sizes, widest first.
        /// </summary>
        [JsonPropertyName("images")]
        public Image[] Images { get; set; }

        /// <summary>
        /// The popularity of the artist. The value will be between 0 and 100, with 100 being the most popular.
        /// The artist’s popularity is calculated from the popularity of all the artist’s tracks.
        /// </summary>
        [JsonPropertyName("popularity")]
        public int Popularity { get; set; }
    }
}
