using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;
using FluentSpotifyApi.Model.Artists;

namespace FluentSpotifyApi.Model.Albums
{
    /// <summary>
    /// The base class for albums.
    /// </summary>
    /// <seealso cref="EntityBase" />
    public abstract class AlbumBase : EntityBase
    {
        /// <summary>
        /// The type of the album: <c>album</c>, <c>single</c>, or <c>compilation</c>.
        /// </summary>
        [JsonPropertyName("album_type")]
        public string AlbumType { get; set; }

        /// <summary>
        /// The artists of the album. Each artist object includes a link in <see cref="EntityBase.Href"/> to more detailed information about the artist.
        /// </summary>
        [JsonPropertyName("artists")]
        public SimplifiedArtist[] Artists { get; set; }

        /// <summary>
        /// The markets in which the album is available: ISO 3166-1 alpha-2 country codes.
        /// Note that an album is considered available in a market when at least 1 of its tracks is available in that market.
        /// </summary>
        [JsonPropertyName("available_markets")]
        public string[] AvailableMarkets { get; set; }

        /// <summary>
        /// The cover art for the album in various sizes, widest first.
        /// </summary>
        [JsonPropertyName("images")]
        public Image[] Images { get; set; }

        /// <summary>
        /// The name of the album. In case of an album take down, the value may be an empty string.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The date the album was first released, for example 1981.
        /// Depending on the precision, it might be shown as 1981-12 or 1981-12-15.
        /// </summary>
        [JsonPropertyName("release_date")]
        public string ReleaseDate { get; set; }

        /// <summary>
        /// The precision with which release_date value is known: year, month, or day.
        /// </summary>
        [JsonPropertyName("release_date_precision")]
        public string ReleaseDatePrecision { get; set; }

        /// <summary>
        /// Included in the response when a content restriction is applied.
        /// </summary>
        [JsonPropertyName("restrictions")]
        public Restriction Restrictions { get; set; }
    }
}
