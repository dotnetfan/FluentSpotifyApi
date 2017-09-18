using System.Collections.Generic;
using FluentSpotifyApi.Builder.Me.Library;
using FluentSpotifyApi.Model;

namespace FluentSpotifyApi.Builder.Me
{
    /// <summary>
    /// Gets builder for "me/albums" and "me/tracks" endpoints.
    /// These endpoints are used for retrieving information about, and managing, tracks and albums 
    /// that the current user has saved in their “Your Music” library.
    /// </summary>
    public interface ILibraryBuilder
    {
        /// <summary>
        /// Gets builder for "me/tracks" endpoint.
        /// </summary>
        IGetLibraryEntitiesBuilder<SavedTrack> Tracks();

        /// <summary>
        /// Gets builder for "me/tracks" endpoint with IDs.
        /// </summary>
        /// <param name="ids">
        /// The track IDs. Maximum: 50.
        /// </param>
        IManageLibraryEntitiesBuilder Tracks(IEnumerable<string> ids);

        /// <summary>
        /// Gets builder for "me/albums" endpoint.
        /// </summary>
        IGetLibraryEntitiesBuilder<SavedAlbum> Albums();

        /// <summary>
        /// Gets builder for "me/albums" endpoint with IDs.
        /// </summary>
        /// <param name="ids">
        /// The album IDs. Maximum: 50.
        /// </param>
        IManageLibraryEntitiesBuilder Albums(IEnumerable<string> ids);
    }
}
