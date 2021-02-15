using System;
using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;
using FluentSpotifyApi.Model.Tracks;

namespace FluentSpotifyApi.Model.Player
{
    /// <summary>
    /// The play history.
    /// </summary>
    public class PlayHistory : JsonObject
    {
        /// <summary>
        /// The context the track was played from.
        /// </summary>
        [JsonPropertyName("context")]
        public Context Context { get; set; }

        /// <summary>
        /// The UTC date and time the track was played.
        /// </summary>
        [JsonPropertyName("played_at")]
        public DateTime PlayedAt { get; set; }

        /// <summary>
        /// The track the user listened to.
        /// </summary>
        [JsonPropertyName("track")]
        public SimplifiedTrack Track { get; set; }
    }
}
