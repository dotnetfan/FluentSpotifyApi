using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.Model.Player
{
    /// <summary>
    /// The actions.
    /// </summary>
    public class Actions : JsonObject
    {
        /// <summary>
        /// The disallows.
        /// </summary>
        [JsonPropertyName("disallows")]
        public Disallows Disallows { get; set; }
    }
}
