namespace FluentSpotifyApi.Builder.Me.Player
{
    /// <summary>
    /// The builder for starting new playback at specified offset.
    /// </summary>
    /// <seealso cref="FluentSpotifyApi.Builder.Me.Player.IPlaybackPlayBuilder" />
    public interface IPlaybackPlayWithOffsetBuilder : IPlaybackPlayBuilder
    {
        /// <summary>
        /// Gets builder for starting new playback at specified track.
        /// </summary>
        /// <param name="trackId">The track ID.</param>
        /// <returns></returns>
        IPlaybackPlayBuilder StartAt(string trackId);

        /// <summary>
        /// Gets builder for starting new playback at specified position.
        /// </summary>
        /// <param name="position">The zero based position.</param>
        /// <returns></returns>
        IPlaybackPlayBuilder StartAt(int position);
    }
}
