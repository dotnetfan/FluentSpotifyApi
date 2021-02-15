using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FluentSpotifyApi.Builder.Me.Player
{
    /// <summary>
    /// The builder for the playback.
    /// </summary>
    public interface IPlaybackBuilder
    {
        /// <summary>
        /// Gets builder for "me/player/queue" endpoint.
        /// </summary>
        IPlaybackQueueBuilder Queue { get; }

        /// <summary>
        /// Starts new playback.
        /// </summary>
        /// <param name="contextUri">The Spotify URI of artist, album or playlist. Use <see cref="SpotifyUri"/> to get URI from ID.</param>
        /// <param name="offset">The zero based position of an item in <paramref name="contextUri"/> where the playback will be started.</param>
        /// <param name="position">The time position within the item where the playback will be started.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task StartNewAsync(string contextUri, int? offset = null, TimeSpan? position = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Starts new playback.
        /// </summary>
        /// <param name="uris">The Spotify URIs of track or episodes. Use <see cref="SpotifyUri"/> to get URI from ID.</param>
        /// <param name="offset">The Spotify URI of track o episode where the playback will be started. Use <see cref="SpotifyUri"/> to get URI from ID.</param>
        /// <param name="position">The time position within the track or episode where the playback will be started.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task StartNewAsync(IEnumerable<string> uris, string offset = null, TimeSpan? position = null, CancellationToken cancellationToken = default(CancellationToken));

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
        /// <param name="position">
        /// The time position to seek to. Must be a positive number.
        /// Passing in a position that is greater than the length of the track will cause the player to start playing the next song.</param>
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
        /// <param name="volumePercent">The volume to set. Must be a value from 0 to 100 inclusive.</param>
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
