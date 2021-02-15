using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.Model.Playlists
{
    /// <summary>
    /// The request for creating new playlists.
    /// </summary>
    public class CreatePlaylistRequest : JsonObject
    {
        /// <summary>
        /// The required name for the new playlist. This name does not need to be unique; a user may have several playlists with the same name.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Defaults to <c>true</c>. If <c>true</c> the playlist will be public, if <c>false</c> it will be private.
        /// To be able to create private playlists, the user must have granted the <c>playlist-modify-private</c> scope.
        /// </summary>
        [JsonPropertyName("public")]
        public bool? Public { get; set; }

        /// <summary>
        /// Defaults to <c>false</c>. If <c>true</c> the playlist will be collaborative. Note that to create a collaborative playlist you must also set <see cref="Public"/> to <c>false</c>.
        /// To create collaborative playlists you must have granted <c>playlist-modify-private</c> and <c>playlist-modify-public scopes</c>.
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
