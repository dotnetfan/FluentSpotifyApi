using FluentSpotifyApi.Model.Albums;
using FluentSpotifyApi.Model.Artists;
using FluentSpotifyApi.Model.Episodes;
using FluentSpotifyApi.Model.Playlists;
using FluentSpotifyApi.Model.Search;
using FluentSpotifyApi.Model.Shows;
using FluentSpotifyApi.Model.Tracks;

namespace FluentSpotifyApi.Builder.Search
{
    /// <summary>
    /// The builder for "search" endpoint.
    /// </summary>
    public interface ISearchBuilder
    {
        /// <summary>
        /// Gets builder for "search?type=album" endpoint.
        /// </summary>
        ISearchTypeBuilder<SimplifiedAlbumsPageResponse> Albums { get; }

        /// <summary>
        /// Gets builder for "search?type=artist" endpoint.
        /// </summary>
        ISearchTypeBuilder<ArtistsPageResponse> Artists { get; }

        /// <summary>
        /// Gets builder for "search?type=playlist" endpoint.
        /// </summary>
        ISearchTypeBuilder<SimplifiedPlaylistsPageResponse> Playlists { get; }

        /// <summary>
        /// Gets builder for "search?type=track" endpoint.
        /// </summary>
        ISearchTypeBuilder<TracksPageResponse> Tracks { get; }

        /// <summary>
        /// Gets builder for "search?type=show" endpoint.
        /// </summary>
        ISearchTypeBuilder<SimplifiedShowsPageResponse> Shows { get; }

        /// <summary>
        /// Gets builder for "search?type=episode" endpoint.
        /// </summary>
        ISearchTypeBuilder<SimplifiedEpisodesPageResponse> Episodes { get; }

        /// <summary>
        /// Gets builder for "search?type={entities}" endpoint.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        ISearchTypeBuilder<SearchResponse> Entities(params Entity[] entities);

        /// <summary>
        /// Gets builder for "search?type={allEntities}" endpoint.
        /// </summary>
        /// <returns></returns>
        ISearchTypeBuilder<SearchResponse> Entities();
    }
}
