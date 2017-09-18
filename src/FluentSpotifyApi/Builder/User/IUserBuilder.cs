using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Builder.User.Playlists;
using FluentSpotifyApi.Model;

namespace FluentSpotifyApi.Builder.User
{
    /// <summary>
    /// The builder for "users/{id}" endpoint.
    /// </summary>
    public interface IUserBuilder
    {
        /// <summary>
        /// Gets builder for "users/{id}/playlists" endpoint.
        /// </summary>
        IPlaylistsBuilder Playlists { get; }

        /// <summary>
        /// Gets builder for "users/{userId}/playlists/{id}" endpoint.
        /// </summary>
        /// <param name="id">The playlist ID.</param>
        IPlaylistBuilder Playlist(string id);

        /// <summary>
        /// Get public profile information about a Spotify user.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<PublicUser> GetAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
