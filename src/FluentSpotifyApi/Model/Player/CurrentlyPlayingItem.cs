using System;
using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;
using FluentSpotifyApi.Core.Serialization;

namespace FluentSpotifyApi.Model.Player
{
    /// <summary>
    /// The currently playing item.
    /// </summary>
    public class CurrentlyPlayingItem : JsonObject
    {
        /// <summary>
        /// Allows to update the user interface based on which playback actions are available within the current context.
        /// </summary>
        [JsonPropertyName("actions")]
        public Actions Actions { get; set; }

        /// <summary>
        /// A Context Object. Can be <c>null</c>.
        /// </summary>
        [JsonPropertyName("context")]
        public Context Context { get; set; }

        /// <summary>
        /// The object type of the currently playing item. Can be one of <c>track</c>, <c>episode</c>, <c>ad</c> or <c>unknown</c>.
        /// </summary>
        [JsonPropertyName("currently_playing_type")]
        public string CurrentlyPlayingType { get; set; }

        /// <summary>
        /// If something is currently playing, return <c>true</c>.
        /// </summary>
        [JsonPropertyName("is_playing")]
        public bool IsPlaying { get; set; }

        /// <summary>
        /// The currently playing <see cref="Tracks.Track"/> or <see cref="Episodes.Episode"/>. Can be <c>null</c>.
        /// </summary>
        [JsonPropertyName("item")]
        public EntityBase Item { get; set; }

        /// <summary>
        /// Progress into the currently playing track or episode. Can be <c>null</c>.
        /// </summary>
        [JsonPropertyName("progress_ms")]
        [JsonConverter(typeof(SpotifyTimeSpanMillisecondsConverter))]
        public TimeSpan? Progress { get; set; }

        /// <summary>
        /// The date and time when data was fetched.
        /// </summary>
        [JsonPropertyName("timestamp")]
        [JsonConverter(typeof(SpotifyTimestampMillisecondsConverter))]
        public DateTime Timestamp { get; set; }
    }
}
