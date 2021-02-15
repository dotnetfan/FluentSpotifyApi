using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.Model.Tracks
{
    /// <summary>
    /// The tracks page response.
    /// </summary>
    public class TracksPageResponse : JsonObject
    {
        /// <summary>
        /// The tracks page.
        /// </summary>
        [JsonPropertyName("tracks")]
        public Page<Track> Page { get; set; }
    }
}
