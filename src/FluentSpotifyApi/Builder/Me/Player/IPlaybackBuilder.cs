using System;
using System.Threading;
using System.Threading.Tasks;

namespace FluentSpotifyApi.Builder.Me.Player
{
    /// <summary>
    /// The playback builder.
    /// </summary>
    public interface IPlaybackBuilder
    {
        /// <summary>
        /// Gets builder for starting new playback from specified entities.
        /// </summary>
        IPlaybackFromBuilder From { get; }

        /// <summary>
        /// Resumes user's playback.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task ResumeAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Pauses user's playback.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task PauseAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Skips to next track in the user’s queue.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task SkipToNextAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Skips to previous track in the user’s queue.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task SkipToPreviousAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Seeks to the given position in the user’s currently playing track.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task SeekAsync(TimeSpan position, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Sets the repeat mode for user’s playback.
        /// </summary>
        /// <param name="repeatMode">The repeat mode.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task SetRepeatModeAsync(RepeatMode repeatMode, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Sets the volume for user’s playback.
        /// </summary>
        /// <param name="volumePercent">The volume in percent.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task SetVolumeAsync(int volumePercent, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Turns shuffle on for user's playback.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task TurnOnShuffleAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Turns shuffle off for user's playback.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task TurnOffShuffleAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
