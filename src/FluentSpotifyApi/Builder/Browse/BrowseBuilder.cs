#pragma warning disable SA1201 // Elements should appear in the correct order

using FluentSpotifyApi.Extensions;

namespace FluentSpotifyApi.Builder.Browse
{
    internal class BrowseBuilder : BuilderBase, IBrowseBuilder
    {
        public BrowseBuilder(RootBuilder root)
            : base(root, "browse".Yield())
        {
        }

        public IBrowseCategoriesBuilder Categories() => new BrowseCategoriesBuilder(this);

        public IBrowseCategoryBuilder Categories(string id) => new BrowseCategoryBuilder(this, id);

        public IBrowseFeaturedPlaylistsBuilder FeaturedPlaylists => new BrowseFeaturedPlaylistsBuilder(this);

        public IBrowseNewReleasesBuilder NewReleases => new BrowseNewReleasesBuilder(this);

        public IBrowseRecommendationsBuilder Recommendations => new BrowseRecommendationsBuilder(this.CreateRootBuilder());
    }
}
