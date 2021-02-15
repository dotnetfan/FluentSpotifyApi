using System;
using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;
using FluentSpotifyApi.Model.Albums;

namespace FluentSpotifyApi.Model.Library
{
    /// <summary>
    /// The saved album.
    /// </summary>
    public class SavedAlbum : JsonObject
    {
        /// <summary>
        /// The UTC date and time the album was saved.
        /// </summary>
        [JsonPropertyName("added_at")]
        public DateTime AddedAt { get; set; }

        /// <summary>
        /// Information about the album.
        /// </summary>
        [JsonPropertyName("album")]
        public Album Album { get; set; }
    }
}
