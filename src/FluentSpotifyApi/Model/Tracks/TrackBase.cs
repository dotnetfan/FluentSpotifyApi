using System;
using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;
using FluentSpotifyApi.Core.Serialization;
using FluentSpotifyApi.Model.Artists;

namespace FluentSpotifyApi.Model.Tracks
{
    /// <summary>
    /// The base class for tracks.
    /// </summary>
    /// <seealso cref="EntityBase" />
    public abstract class TrackBase : EntityBase
    {
        /// <summary>
        /// The artists who performed the track. Each artist object includes a link in <see cref="EntityBase.Href"/> to more detailed information about the artist.
        /// </summary>
        [JsonPropertyName("artists")]
        public SimplifiedArtist[] Artists { get; set; }

        /// <summary>
        /// A list of the countries in which the track can be played, identified by their ISO 3166-1 alpha-2 code.
        /// </summary>
        [JsonPropertyName("available_markets")]
        public string[] AvailableMarkets { get; set; }

        /// <summary>
        /// The disc number (usually <c>1</c> unless the album consists of more than one disc).
        /// </summary>
        [JsonPropertyName("disc_number")]
        public int DiscNumber { get; set; }

        /// <summary>
        /// The track length.
        /// </summary>
        [JsonPropertyName("duration_ms")]
        [JsonConverter(typeof(SpotifyTimeSpanMillisecondsConverter))]
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// Whether or not the track has explicit lyrics (<c>true</c> = yes it does; <c>false</c> = no it does not OR unknown).
        /// </summary>
        [JsonPropertyName("explicit")]
        public bool IsExplicit { get; set; }

        /// <summary>
        /// Part of the response when Track Relinking is applied.
        /// If <c>true</c>, the track is playable in the given market. Otherwise <c>false</c>.
        /// </summary>
        [JsonPropertyName("is_playable")]
        public bool IsPlayable { get; set; }

        /// <summary>
        /// Part of the response when Track Relinking is applied, and the requested track has been replaced with different track.
        /// The track in the <see cref="LinkedTrack"/> object contains information about the originally requested track.
        /// </summary>
        [JsonPropertyName("linked_from")]
        public LinkedTrack LinkedFrom { get; set; }

        /// <summary>
        /// Included in the response when a content restriction is applied.
        /// </summary>
        [JsonPropertyName("restrictions")]
        public Restriction Restrictions { get; set; }

        /// <summary>
        /// The name of the track.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// A link to a 30 second preview (MP3 format) of the track. Can be <c>null</c>.
        /// </summary>
        [JsonPropertyName("preview_url")]
        public string PreviewUrl { get; set; }

        /// <summary>
        /// The number of the track. If an album has several discs, the track number is the number on the specified disc.
        /// </summary>
        [JsonPropertyName("track_number")]
        public int TrackNumber { get; set; }

        /// <summary>
        /// Whether or not the track is from a local file.
        /// </summary>
        [JsonPropertyName("is_local")]
        public bool IsLocal { get; set; }
    }
}
