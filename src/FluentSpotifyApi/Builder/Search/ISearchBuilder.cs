using FluentSpotifyApi.Model;
using FluentSpotifyApi.Model.Messages;

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
        ISearchTypeBuilder<SimpleAlbumsPageMessage> Albums { get; }

        /// <summary>
        /// Gets builder for "search?type=artist" endpoint.
        /// </summary>
        ISearchTypeBuilder<FullArtistsPageMessage> Artists { get; }

        /// <summary>
        /// Gets builder for "search?type=playlist" endpoint.
        /// </summary>
        ISearchTypeBuilder<SimplePlaylistsPageMessage> Playlists { get; }

        /// <summary>
        /// Gets builder for "search?type=track" endpoint.
        /// </summary>
        ISearchTypeBuilder<FullTracksPageMessage> Tracks { get; }

        /// <summary>
        /// Gets builder for "search?type={entities}" endpoint.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        ISearchTypeBuilder<SearchResult> Entities(params Entity[] entities);

        /// <summary>
        /// Gets builder for "search?type={allEntities}" endpoint.
        /// </summary>
        /// <returns></returns>
        ISearchTypeBuilder<SearchResult> Entities();
    }
}
