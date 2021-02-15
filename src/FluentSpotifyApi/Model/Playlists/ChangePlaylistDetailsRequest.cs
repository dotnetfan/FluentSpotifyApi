using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.Model.Playlists
{
    /// <summary>
    /// The request for changing playlist's details.
    /// </summary>
    public sealed class ChangePlaylistDetailsRequest : JsonObject
    {
        /// <summary>
        /// The optional new name for the playlist.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Optional. If <c>true</c> the playlist will be public, if <c>false</c> it will be private.
        /// </summary>
        [JsonPropertyName("public")]
        public bool? Public { get; set; }

        /// <summary>
        /// Optional. If <c>true</c>, the playlist will become collaborative and other users will be able to modify the playlist in their Spotify client.
        /// Note: You can only set collaborative to <c>true</c> on non-public playlists.
        /// </summary>
        [JsonPropertyName("collaborative")]
        public bool? Collaborative { get; set; }

        /// <summary>
        /// Optional value for playlist description as displayed in Spotify Clients and in the Web API.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
