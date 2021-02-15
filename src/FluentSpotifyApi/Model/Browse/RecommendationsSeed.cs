using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.Model.Browse
{
    /// <summary>
    /// The recommendations seed.
    /// </summary>
    public class RecommendationsSeed : JsonObject
    {
        /// <summary>
        /// The number of tracks available after min_* and max_* filters have been applied.
        /// </summary>
        [JsonPropertyName("afterFilteringSize")]
        public int AfterFilteringSize { get; set; }

        /// <summary>
        /// The number of tracks available after relinking for regional availability.
        /// </summary>
        [JsonPropertyName("afterRelinkingSize")]
        public int AfterRelinkingSize { get; set; }

        /// <summary>
        /// A link to the full track or artist data for this seed. For tracks this will be a link to a track.
        /// For artists a link to an <see cref="Artists.Artist"/>.
        /// For genre seeds, this value will be <c>null</c>.
        /// </summary>
        [JsonPropertyName("href")]
        public string Href { get; set; }

        /// <summary>
        /// The id used to select this seed. This will be the same as the string used in the <c>seed_artists</c>, <c>seed_tracks</c> or <c>seed_genres</c> parameter.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The number of recommended tracks available for this seed.
        /// </summary>
        [JsonPropertyName("initialPoolSize")]
        public int InitialPoolSize { get; set; }

        /// <summary>
        /// The entity type of this seed. One of <c>artist</c>, <c>track</c> or <c>genre</c>.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
