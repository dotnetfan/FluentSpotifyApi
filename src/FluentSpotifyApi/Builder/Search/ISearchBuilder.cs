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
        ISearchEntityBuilder<SimpleAlbumsPageMessage> Albums { get; }

        /// <summary>
        /// Gets builder for "search?type=artist" endpoint.
        /// </summary>
        ISearchEntityBuilder<FullArtistsPageMessage> Artists { get; }

        /// <summary>
        /// Gets builder for "search?type=playlist" endpoint.
        /// </summary>
        ISearchEntityBuilder<SimplePlaylistsPageMessage> Playlists { get; }

        /// <summary>
        /// Gets builder for "search?type=track" endpoint.
        /// </summary>
        ISearchEntityBuilder<FullTracksPageMessage> Tracks { get; }

        /// <summary>
        /// Gets builder for "search?type={entities}" endpoint.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        ISearchEntitiesBuilder Entities(params Entity[] entities);

        /// <summary>
        /// Gets builder for "search?type={allEntities}" endpoint.
        /// </summary>
        /// <returns></returns>
        ISearchEntitiesBuilder Entities();
    }
}
