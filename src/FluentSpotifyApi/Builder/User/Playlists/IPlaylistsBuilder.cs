using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model;

namespace FluentSpotifyApi.Builder.User.Playlists
{
    /// <summary>
    /// The builder for "users/{id}/playlists" endpoint.
    /// </summary>
    public interface IPlaylistsBuilder
    {
        /// <summary>
        /// Get a list of the playlists owned or followed by a Spotify user.
        /// </summary>
        /// <param name="limit">The maximum number of playlists to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">The index of the first playlist to return. Default: 0 (the first object). Maximum offset: 100.000.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<Page<SimplePlaylist>> GetAsync(int limit = 20, int offset = 0, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Create a playlist for a Spotify user. (The playlist will be empty until you add tracks.)
        /// </summary>
        /// <param name="createPlaylistDto">New playlist DTO.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<FullPlaylist> CreateAsync(CreatePlaylistDto createPlaylistDto, CancellationToken cancellationToken = default(CancellationToken));
    }
}
