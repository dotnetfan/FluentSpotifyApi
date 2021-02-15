using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;
using FluentSpotifyApi.Model.Artists;

namespace FluentSpotifyApi.Model.Following
{
    /// <summary>
    /// The followed artists.
    /// </summary>
    public class FollowedArtists : JsonObject
    {
        /// <summary>
        /// The followed artists page.
        /// </summary>
        [JsonPropertyName("artists")]
        public CursorBasedPage<Artist> Page { get; set; }
    }
}
