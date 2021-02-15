using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.Model.Artists
{
    /// <summary>
    /// The base class for artists.
    /// </summary>
    /// <seealso cref="EntityBase" />
    public abstract class ArtistBase : EntityBase
    {
        /// <summary>
        /// The name of the artist.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
