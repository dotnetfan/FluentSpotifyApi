using System;
using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;
using FluentSpotifyApi.Model.Shows;

namespace FluentSpotifyApi.Model.Library
{
    /// <summary>
    /// The saved show.
    /// </summary>
    public class SavedShow : JsonObject
    {
        /// <summary>
        /// The UTC date and time the show was saved.
        /// </summary>
        [JsonPropertyName("added_at")]
        public DateTime AddedAt { get; set; }

        /// <summary>
        /// Gets or sets the show.
        /// </summary>
        /// <value>
        /// The show.
        /// </value>
        [JsonPropertyName("show")]
        public SimplifiedShow Show { get; set; }
    }
}
