using FluentSpotifyApi.Model.Artists;
using FluentSpotifyApi.Model.Tracks;

namespace FluentSpotifyApi.Builder.Me.Personalization
{
    /// <summary>
    /// The builder for "me/top" endpoint.
    /// This endpoint is used for retrieving information about the user’s listening habits.
    /// </summary>
    public interface IPersonalizationBuilder
    {
        /// <summary>
        /// Gets builder for "me/top/artists" endpoint.
        /// </summary>
        ITopBuilder<Artist> TopArtists { get; }

        /// <summary>
        /// Gets builder for "me/top/tracks" endpoint.
        /// </summary>
        ITopBuilder<Track> TopTracks { get; }
    }
}
