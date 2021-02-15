using System.Text.Json.Serialization;

namespace FluentSpotifyApi.Model.Playlists
{
    /// <summary>
    /// The simplified playlist.
    /// </summary>
    /// <seealso cref="PlaylistBase" />
    public class SimplifiedPlaylist : PlaylistBase
    {
        /// <summary>
        /// A collection containing a link ( <see cref="PlaylistTracksRef.Href"/> ) to the Web API endpoint where full details of the playlist’s tracks can be retrieved,
        /// along with the <see cref="PlaylistTracksRef.Total"/> number of tracks in the playlist.
        /// Note, a track object may be <c>null</c>. This can happen if a track is no longer available.
        /// </summary>
        [JsonPropertyName("tracks")]
        public PlaylistTracksRef Tracks { get; set; }
    }
}
