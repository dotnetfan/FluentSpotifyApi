using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.Model.Tracks
{
    /// <summary>
    /// The tracks response.
    /// </summary>
    public class TracksResponse : JsonObject
    {
        /// <summary>
        /// The tracks.
        /// </summary>
        [JsonPropertyName("tracks")]
        public Track[] Items { get; set; }
    }
}
