using System;
using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;
using FluentSpotifyApi.Model.Tracks;

namespace FluentSpotifyApi.Model.Library
{
    /// <summary>
    /// The saved track.
    /// </summary>
    public class SavedTrack : JsonObject
    {
        /// <summary>
        /// Gets or sets the added at.
        /// </summary>
        /// <value>
        /// The added at.
        /// </value>
        [JsonPropertyName("added_at")]
        public DateTime AddedAt { get; set; }

        /// <summary>
        /// Gets or sets the track.
        /// </summary>
        /// <value>
        /// The track.
        /// </value>
        [JsonPropertyName("track")]
        public Track Track { get; set; }
    }
}
