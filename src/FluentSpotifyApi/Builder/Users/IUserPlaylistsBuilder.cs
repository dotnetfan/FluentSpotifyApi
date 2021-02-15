using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model;
using FluentSpotifyApi.Model.Playlists;

namespace FluentSpotifyApi.Builder.Users
{
    /// <summary>
    /// The builder for "users/{id}/playlists" endpoint.
    /// </summary>
    public interface IUserPlaylistsBuilder
    {
        /// <summary>
        /// Gets a list of the playlists owned or followed by a Spotify user.
        /// </summary>
        /// <param name="limit">The maximum number of playlists to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">
        /// The index of the first playlist to return. Default: 0 (the first object). Maximum offset: 100.000.
        /// Use with <paramref name="limit"/> to get the next set of playlists.
        /// </param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<Page<SimplifiedPlaylist>> GetAsync(int? limit = null, int? offset = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Creates a playlist for a Spotify user. (The playlist will be empty until you add tracks.)
        /// </summary>
        /// <param name="createPlaylistRequest">New playlist request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<Playlist> CreateAsync(CreatePlaylistRequest createPlaylistRequest, CancellationToken cancellationToken = default(CancellationToken));
    }
}
