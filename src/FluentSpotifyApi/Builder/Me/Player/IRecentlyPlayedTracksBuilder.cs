using System;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model;
using FluentSpotifyApi.Model.Player;

namespace FluentSpotifyApi.Builder.Me.Player
{
    /// <summary>
    /// The builder for "me/player/recently-played" endpoint.
    /// </summary>
    public interface IRecentlyPlayedTracksBuilder
    {
        /// <summary>
        /// Get tracks from the current user’s recently played tracks.
        /// </summary>
        /// <param name="limit">The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="after">
        /// Returns all items after (but not including) this cursor position.
        /// If <paramref name="after"/> is specified, <paramref name="before"/> must not be specified.
        /// </param>
        /// <param name="before">
        /// Returns all items before (but not including) this cursor position.
        /// If <paramref name="before"/> is specified, <paramref name="after"/> must not be specified.
        /// </param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<CursorBasedPage<PlayHistory>> GetAsync(int? limit = null, DateTime? after = null, DateTime? before = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}
