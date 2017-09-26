using System.Threading;
using System.Threading.Tasks;

namespace FluentSpotifyApi.Builder.Me.Player
{
    /// <summary>
    /// The builder for starting new playback.
    /// </summary>
    public interface IPlaybackPlayBuilder
    {
        /// <summary>
        /// Starts new playback.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task PlayAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
