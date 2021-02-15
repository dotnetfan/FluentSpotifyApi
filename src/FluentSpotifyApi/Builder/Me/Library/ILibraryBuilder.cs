using FluentSpotifyApi.Model.Library;

namespace FluentSpotifyApi.Builder.Me.Library
{
    /// <summary>
    /// The builder for "me/albums", "me/tracks" and "me/shows" endpoints.
    /// These endpoints are used for retrieving information about, and managing, tracks, albums and shows
    /// that the current user has saved in their “Your Music” library.
    /// </summary>
    public interface ILibraryBuilder
    {
        /// <summary>
        /// Gets builder for "me/albums" endpoint.
        /// </summary>
        ILibraryItemsBuilder<SavedAlbum> Albums { get; }

        /// <summary>
        /// Gets builder for "me/tracks" endpoint.
        /// </summary>
        ILibraryItemsBuilder<SavedTrack> Tracks { get; }

        /// <summary>
        /// Gets builder for "me/tracks" endpoint.
        /// </summary>
        ILibraryItemsBuilder<SavedShow> Shows { get; }
    }
}
