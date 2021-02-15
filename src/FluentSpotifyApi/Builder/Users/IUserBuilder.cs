using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.Builder.Users
{
    /// <summary>
    /// The builder for "users/{id}" endpoint.
    /// </summary>
    public interface IUserBuilder
    {
        /// <summary>
        /// Gets builder for "users/{id}/playlists" endpoint.
        /// </summary>
        IUserPlaylistsBuilder Playlists { get; }

        /// <summary>
        /// Gets public profile information about a Spotify user.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<PublicUser> GetAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
