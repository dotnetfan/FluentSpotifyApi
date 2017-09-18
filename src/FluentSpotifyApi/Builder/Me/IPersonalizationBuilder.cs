using FluentSpotifyApi.Builder.Me.Personalization.RecentlyPlayed;
using FluentSpotifyApi.Builder.Me.Personalization.Top;
using FluentSpotifyApi.Model;

namespace FluentSpotifyApi.Builder.Me
{
    /// <summary>
    /// The builder for "me/top" and "me/player" endpoints.
    /// These endpoints are used for retrieving information about the user’s listening habits.
    /// </summary>
    public interface IPersonalizationBuilder
    {
        /// <summary>
        /// Gets builder for "me/top/artists" endpoint.
        /// </summary>
        ITopBuilder<FullArtist> TopArtists { get; }

        /// <summary>
        /// Gets builder for "me/top/tracks" endpoint.
        /// </summary>
        ITopBuilder<FullTrack> TopTracks { get; }

        /// <summary>
        /// Gets builder for "me/player/recently-played" endpoint.
        /// </summary>
        IRecentlyPlayedTracksBuilder RecentlyPlayedTracks { get; }
    }
}
