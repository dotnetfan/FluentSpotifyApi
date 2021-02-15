using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.Model.Shows
{
    /// <summary>
    /// The simplified shows page response.
    /// </summary>
    public class SimplifiedShowsPageResponse : JsonObject
    {
        /// <summary>
        /// The simplified shows page.
        /// </summary>
        [JsonPropertyName("shows")]
        public Page<SimplifiedShow> Page { get; set; }
    }
}
