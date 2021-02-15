using System;
using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;
using FluentSpotifyApi.Core.Serialization;

namespace FluentSpotifyApi.Model.Episodes
{
    /// <summary>
    /// The base class for episodes.
    /// </summary>
    /// <seealso cref="EntityBase" />
    public abstract class EpisodeBase : EntityBase
    {
        /// <summary>
        /// A URL to a 30 second preview (MP3 format) of the episode. <c>null</c> if not available.
        /// </summary>
        [JsonPropertyName("audio_preview_url")]
        public string AudioPreviewUrl { get; set; }

        /// <summary>
        /// A description of the episode.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// The episode length.
        /// </summary>
        [JsonPropertyName("duration_ms")]
        [JsonConverter(typeof(SpotifyTimeSpanMillisecondsConverter))]
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// Whether or not the episode has explicit content (true = yes it does; false = no it does not OR unknown).
        /// </summary>
        [JsonPropertyName("explicit")]
        public bool IsExplicit { get; set; }

        /// <summary>
        /// The cover art for the episode in various sizes, widest first.
        /// </summary>
        [JsonPropertyName("images")]
        public Image[] Images { get; set; }

        /// <summary>
        /// True if the episode is hosted outside of Spotify’s CDN.
        /// </summary>
        [JsonPropertyName("is_externally_hosted")]
        public bool IsExternallyHosted { get; set; }

        /// <summary>
        /// True if the episode is playable in the given market. Otherwise false.
        /// </summary>
        [JsonPropertyName("is_playable")]
        public bool IsPlayable { get; set; }

        /// <summary>
        /// A list of the languages used in the episode, identified by their ISO 639 code.
        /// </summary>
        [JsonPropertyName("languages")]
        public string[] Languages { get; set; }

        /// <summary>
        /// The name of the episode.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The date the episode was first released, for example <c>"1981-12-15"</c>. Depending on the precision, it might be shown as <c>"1981"</c> or <c>"1981-12"</c>.
        /// </summary>
        [JsonPropertyName("release_date")]
        public string ReleaseDate { get; set; }

        /// <summary>
        /// The precision with which <see cref="ReleaseDate"/> value is known: <c>"year"</c>, <c>"month"</c>, or <c>"day"</c>.
        /// </summary>
        [JsonPropertyName("release_date_precision")]
        public string ReleaseDatePrecision { get; set; }

        /// <summary>
        /// The user’s most recent position in the episode. Set if the supplied access token is a user token and has the scope <c>user-read-playback-position</c>.
        /// </summary>
        [JsonPropertyName("resume_point")]
        public ResumePoint ResumePoint { get; set; }
    }
}
