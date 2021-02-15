using System.Text.Json.Serialization;
using FluentSpotifyApi.Model.Episodes;

namespace FluentSpotifyApi.Model.Shows
{
    /// <summary>
    /// The show.
    /// </summary>
    /// <seealso cref="ShowBase"/>
    public class Show : ShowBase
    {
        /// <summary>
        /// The show’s episodes.
        /// </summary>
        [JsonPropertyName("episodes")]
        public Page<SimplifiedEpisode> Episodes { get; set; }
    }
}
