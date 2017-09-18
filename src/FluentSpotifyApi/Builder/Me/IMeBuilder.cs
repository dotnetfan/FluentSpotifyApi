using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Builder.User.Playlists;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.Builder.Me
{
    /// <summary>
    /// The builder for "me" and "users/{myUserId}/playlists" endpoints.
    /// </summary>
    public interface IMeBuilder
    {
        /// <summary>
        /// Gets builder for "me/albums" and "me/tracks" endpoints.
        /// These endpoints are used for retrieving information about, and managing, tracks and albums 
        /// that the current user has saved in their “Your Music” library.
        /// </summary>
        ILibraryBuilder Library { get; }

        /// <summary>
        /// Gets builder for "me/top" and "me/player" endpoints.
        /// These endpoints are used for retrieving information about the user’s listening habits.
        /// </summary>
        IPersonalizationBuilder Personalization { get; }

        /// <summary>
        /// Gets builder for "me/following" and "users/{ownerId}/playlists/{playlistId}/followers" endpoints.
        /// These endpoints allow you manage the artists, users and playlists that a Spotify user follows.
        /// </summary>
        IFollowingBuilder Following { get; }

        /// <summary>
        /// Gets builder for users/{myUserId}/playlists endpoint.
        /// </summary>
        IPlaylistsBuilder Playlists { get; }

        /// <summary>
        /// Gets builder for users/{myUserId}/playlists/{id} endpoint.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The builder for users/{myUserId}/playlists/{id} endpoint.</returns>
        IPlaylistBuilder Playlist(string id);

        /// <summary>
        /// Get detailed profile information about the current user (including the current user’s username).
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<PrivateUser> GetAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
