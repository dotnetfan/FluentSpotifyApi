using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.Model.Playlists
{
    /// <summary>
    /// Spotify URI of playlist item with positions.
    /// </summary>
    public class UriWithPositions : JsonObject
    {
        /// <summary>
        /// The Spotify URI of playlist item (track or episode).
        /// </summary>
        [JsonPropertyName("uri")]
        public string Uri { get; set; }

        /// <summary>
        /// Zero based index of playlist item.
        /// </summary>
        [JsonPropertyName("positions")]
        public int[] Positions { get; set; }
    }
}
