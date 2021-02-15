#pragma warning disable SA1201 // Elements should appear in the correct order

namespace FluentSpotifyApi.Builder.Browse
{
    /// <summary>
    /// The builder for "browse" endpoints that are used for getting playlists and new album releases
    /// featured on Spotify’s Browse tab.
    /// </summary>
    public interface IBrowseBuilder
    {
        /// <summary>
        /// Gets builder for "browse/categories" endpoint.
        /// </summary>
        IBrowseCategoriesBuilder Categories();

        /// <summary>
        /// Gets builder for "categories/{id}" endpoint.
        /// </summary>
        /// <param name="id">The category ID.</param>
        IBrowseCategoryBuilder Categories(string id);

        /// <summary>
        /// Gets builder for "browse/featured-playlists" endpoint.
        /// </summary>
        IBrowseFeaturedPlaylistsBuilder FeaturedPlaylists { get; }

        /// <summary>
        /// Gets builder for "browse/new-releases" endpoint.
        /// </summary>
        IBrowseNewReleasesBuilder NewReleases { get; }

        /// <summary>
        /// Gets builder for "recommendations" endpoint.
        /// </summary>
        IBrowseRecommendationsBuilder Recommendations { get; }
    }
}
