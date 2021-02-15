using System.Text.Json.Serialization;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.Model.Playlists
{
    /// <summary>
    /// The playlist.
    /// </summary>
    /// <seealso cref="PlaylistBase" />
    public class Playlist : PlaylistBase
    {
        /// <summary>
        /// Information about the followers of the playlist.
        /// </summary>
        [JsonPropertyName("followers")]
        public Followers Followers { get; set; }

        /// <summary>
        /// Information about the tracks of the playlist. Note, a track object may be <c>null</c>. This can happen if a track is no longer available.
        /// </summary>
        [JsonPropertyName("tracks")]
        public Page<PlaylistTrack> Tracks { get; set; }
    }
}
