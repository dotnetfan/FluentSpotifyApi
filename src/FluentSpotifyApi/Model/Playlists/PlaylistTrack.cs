using System;
using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.Model.Playlists
{
    /// <summary>
    /// The playlist track.
    /// </summary>
    public class PlaylistTrack : JsonObject
    {
        /// <summary>
        /// The UTC date and time the track or episode was added. Note that some very old playlists may return <c>null</c> in this field.
        /// </summary>
        [JsonPropertyName("added_at")]
        public DateTime? AddedAt { get; set; }

        /// <summary>
        /// The Spotify user who added the track or episode. Note that some very old playlists may return <c>null</c> in this field.
        /// </summary>
        [JsonPropertyName("added_by")]
        public PublicUser AddedBy { get; set; }

        /// <summary>
        /// Whether this track or episode is a local file or not.
        /// </summary>
        [JsonPropertyName("is_local")]
        public bool IsLocal { get; set; }

        /// <summary>
        /// Information about the <see cref="Tracks.Track"/> or <see cref="Episodes.Episode"/>.
        /// </summary>
        [JsonPropertyName("track")]
        public EntityBase Track { get; set; }
    }
}
