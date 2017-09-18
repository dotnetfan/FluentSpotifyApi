using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Builder.Albums;
using FluentSpotifyApi.Builder.Artists;
using FluentSpotifyApi.Builder.Browse;
using FluentSpotifyApi.Builder.Me;
using FluentSpotifyApi.Builder.Search;
using FluentSpotifyApi.Builder.Tracks;
using FluentSpotifyApi.Builder.User;

namespace FluentSpotifyApi
{
    /// <summary>
    /// The fluent Spotify client.
    /// </summary>
    public interface IFluentSpotifyClient
    {
        /// <summary>
        /// Gets builder for "browse" endpoints that are used for getting playlists and new album releases 
        /// featured on Spotify’s Browse tab.
        /// </summary>
        IBrowseBuilder Browse { get; }

        /// <summary>
        /// Gets builder for "me" and "users/{myUserId}/playlists" endpoints.
        /// </summary>
        IMeBuilder Me { get; }

        /// <summary>
        /// Gets builder for "search" endpoint.
        /// </summary>
        /// <value>
        /// The builder for "search" endpoint.
        /// </value>
        ISearchBuilder Search { get; }

        /// <summary>
        /// Gets builder for "albums/{id}" endpoint.
        /// </summary>
        /// <param name="id">The album ID.</param>
        IAlbumBuilder Album(string id);

        /// <summary>
        /// Gets builder for "albums?ids={ids}" endpoint.
        /// </summary>
        /// <param name="ids">The album IDs. Maximum: 20.</param>
        IAlbumsBuilder Albums(IEnumerable<string> ids);

        /// <summary>
        /// Gets builder for "artists/{id}" endpoint.
        /// </summary>
        /// <param name="id">The artist ID.</param>
        IArtistBuilder Artist(string id);

        /// <summary>
        /// Gets builder for "artists?ids={ids}" endpoint.
        /// </summary>
        /// <param name="ids">
        /// The artist IDs. Maximum: 50.</param>
        IArtistsBuilder Artists(IEnumerable<string> ids);

        /// <summary>
        /// Gets builder for "tracks/{id}" endpoint.
        /// </summary>
        /// <param name="id">The track ID.</param>
        ITrackBuilder Track(string id);

        /// <summary>
        /// Gets builder for "tracks?ids={ids}" endpoint.
        /// </summary>
        /// <param name="ids">
        /// The track IDs
        /// Maximum: 50, for <see cref="ITracksBuilder.GetAsync(string, CancellationToken)"/>.
        /// Maximum: 100, for <see cref="ITracksAudioFeaturesBuilder.GetAsync(CancellationToken)"/>.
        /// </param>
        ITracksBuilder Tracks(IEnumerable<string> ids);

        /// <summary>
        /// Gets builder for "users/{id}" endpoint.
        /// </summary>
        /// <param name="id">The user ID.</param>
        IUserBuilder User(string id);

        /// <summary>
        /// Gets the instance of <typeparamref name="T"/> from the <paramref name="url"/>. 
        /// This method can be used for looping through the paged result where the URL of the next page can be accessed from the <see cref="Model.Page{T}.Next"/> property.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">The URL.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<T> GetAsync<T>(Uri url, CancellationToken cancellationToken = default(CancellationToken));
    }
}
