using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Expressions.Fields;
using FluentSpotifyApi.Model.Playlists;

namespace FluentSpotifyApi.Builder.Playlists
{
    /// <summary>
    /// The builder for "playlists/{id}" endpoint.
    /// </summary>
    public interface IPlaylistBuilder
    {
        /// <summary>
        /// Gets builder for "playlists/{id}/images" endpoint.
        /// </summary>
        IPlaylistImagesBuilder Images { get; }

        /// <summary>
        /// Gets builder for "playlists/{id}/tracks" endpoint.
        /// </summary>
        IPlaylistItemsBuilder Items { get;  }

        /// <summary>
        /// Gets a playlist owned by a Spotify user.
        /// </summary>
        /// <param name="fields">A comma-separated list of the fields to return. If omitted, all fields are returned.</param>
        /// <param name="market">
        /// An ISO 3166-1 alpha-2 country code or the string <c>from_token</c>. Provide this parameter if you want to apply Track Relinking.
        /// For episodes, if a valid user access token is specified in the request header, the country associated with the user account will take priority over this parameter.
        /// </param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<Playlist> GetAsync(string fields = null, string market = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets a playlist owned by a Spotify user.
        /// </summary>
        /// <param name="buildFields">
        /// The action for building fields.
        /// The <see cref="FieldsProvider.Get{TInput}(Action{IFieldsBuilder{TInput}})"/> method can be used to get fields in string format.
        /// </param>
        /// <param name="market">
        /// An ISO 3166-1 alpha-2 country code or the string <c>from_token</c>. Provide this parameter if you want to apply Track Relinking.
        /// For episodes, if a valid user access token is specified in the request header, the country associated with the user account will take priority over this parameter.
        /// </param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<Playlist> GetAsync(Action<IFieldsBuilder<Playlist>> buildFields, string market = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Changes a playlist’s name and public/private state. (The user must, of course, own the playlist.)
        /// </summary>
        /// <param name="changePlaylistDetailsRequest">The request for changing playlist details.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task ChangeDetailsAsync(ChangePlaylistDetailsRequest changePlaylistDetailsRequest, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Checks to see if one or more Spotify users are following a specified playlist.
        /// </summary>
        /// <param name="userIds">
        /// The list of user IDs. Maximum: 5.
        /// </param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<bool[]> CheckFollowersAsync(IEnumerable<string> userIds, CancellationToken cancellationToken = default(CancellationToken));
    }
}
