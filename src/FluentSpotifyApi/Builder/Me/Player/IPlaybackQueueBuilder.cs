using System.Threading;
using System.Threading.Tasks;

namespace FluentSpotifyApi.Builder.Me.Player
{
    /// <summary>
    /// The builder for "me/player/queue" endpoint.
    /// </summary>
    public interface IPlaybackQueueBuilder
    {
        /// <summary>
        /// Adds an item to the end of the user’s current playback queue.
        /// </summary>
        /// <param name="uri">
        /// The uri of the item to add to the queue. Must be a track or an episode uri.
        /// Use <see cref="SpotifyUri"/> to get URI from ID.
        /// </param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task AddAsync(string uri, CancellationToken cancellationToken = default(CancellationToken));
    }
}
