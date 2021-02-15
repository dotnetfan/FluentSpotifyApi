using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.Model.Shows
{
    /// <summary>
    /// The base class for shows.
    /// </summary>
    /// <seealso cref="EntityBase" />
    public abstract class ShowBase : EntityBase
    {
        /// <summary>
        /// A list of the countries in which the show can be played, identified by their ISO 3166-1 alpha-2 code.
        /// </summary>
        [JsonPropertyName("available_markets")]
        public string[] AvailableMarkets { get; set; }

        /// <summary>
        /// The copyright statements of the show.
        /// </summary>
        [JsonPropertyName("copyrights")]
        public Copyright[] Copyrights { get; set; }

        /// <summary>
        /// A description of the show.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// Whether or not the show has explicit content (<c>true</c> = yes it does; <c>false</c> = no it does not OR unknown).
        /// </summary>
        [JsonPropertyName("explicit")]
        public bool Explicit { get; set; }

        /// <summary>
        /// The cover art for the show in various sizes, widest first.
        /// </summary>
        [JsonPropertyName("images")]
        public Image[] Images { get; set; }

        /// <summary>
        /// True if all of the show’s episodes are hosted outside of Spotify’s CDN. This field might be <c>null</c> in some cases.
        /// </summary>
        [JsonPropertyName("is_externally_hosted")]
        public bool IsExternallyHosted { get; set; }

        /// <summary>
        /// A list of the languages used in the show, identified by their ISO 639 code.
        /// </summary>
        [JsonPropertyName("languages")]
        public string[] Languages { get; set; }

        /// <summary>
        /// The media type of the show.
        /// </summary>
        [JsonPropertyName("media_type")]
        public string MediaType { get; set; }

        /// <summary>
        /// The name of the episode.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The publisher of the show.
        /// </summary>
        [JsonPropertyName("publisher")]
        public string Publisher { get; set; }
    }
}
