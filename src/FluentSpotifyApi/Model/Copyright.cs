using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.Model
{
    /// <summary>
    /// The copyright.
    /// </summary>
    public class Copyright : JsonObject
    {
        /// <summary>
        /// The copyright text for this content.
        /// </summary>
        [JsonPropertyName("text")]
        public string Text { get; set; }

        /// <summary>
        /// The type of copyright: <c>C</c> = the copyright, <c>P</c> = the sound recording (performance) copyright.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
