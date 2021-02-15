using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;
using FluentSpotifyApi.Model.Albums;
using FluentSpotifyApi.Model.Artists;
using FluentSpotifyApi.Model.Episodes;
using FluentSpotifyApi.Model.Playlists;
using FluentSpotifyApi.Model.Shows;
using FluentSpotifyApi.Model.Tracks;

namespace FluentSpotifyApi.Model.Search
{
    /// <summary>
    /// The search response.
    /// </summary>
    public class SearchResponse : JsonObject
    {
        /// <summary>
        /// The albums.
        /// </summary>
        [JsonPropertyName("albums")]
        public Page<SimplifiedAlbum> Albums { get; set; }

        /// <summary>
        /// The artists.
        /// </summary>
        [JsonPropertyName("artists")]
        public Page<Artist> Artists { get; set; }

        /// <summary>
        /// The tracks.
        /// </summary>
        [JsonPropertyName("tracks")]
        public Page<Track> Tracks { get; set; }

        /// <summary>
        /// The playlists.
        /// </summary>
        [JsonPropertyName("playlists")]
        public Page<SimplifiedPlaylist> Playlists { get; set; }

        /// <summary>
        /// The shows.
        /// </summary>
        [JsonPropertyName("shows")]
        public Page<SimplifiedShow> Shows { get; set; }

        /// <summary>
        /// The episodes.
        /// </summary>
        [JsonPropertyName("episodes")]
        public Page<SimplifiedEpisode> Episodes { get; set; }
    }
}