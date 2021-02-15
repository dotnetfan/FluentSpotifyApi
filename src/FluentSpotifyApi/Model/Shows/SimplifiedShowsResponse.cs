using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.Model.Shows
{
    /// <summary>
    /// The simplified shows response.
    /// </summary>
    public class SimplifiedShowsResponse : JsonObject
    {
        /// <summary>
        /// The simplified shows.
        /// </summary>
        [JsonPropertyName("shows")]
        public SimplifiedShow[] Items { get; set; }
    }
}
