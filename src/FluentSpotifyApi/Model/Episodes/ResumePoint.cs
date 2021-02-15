using System;
using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;
using FluentSpotifyApi.Core.Serialization;

namespace FluentSpotifyApi.Model.Episodes
{
    /// <summary>
    /// The resume point.
    /// </summary>
    public class ResumePoint : JsonObject
    {
        /// <summary>
        /// Whether or not the episode has been fully played by the user.
        /// </summary>
        [JsonPropertyName("fully_played")]
        public bool FullyPlayed { get; set; }

        /// <summary>
        /// The user’s most recent position in the episode.
        /// </summary>
        [JsonPropertyName("resume_position_ms")]
        [JsonConverter(typeof(SpotifyTimeSpanMillisecondsConverter))]
        public TimeSpan ResumePositionMs { get; set; }
    }
}
