namespace FluentSpotifyApi.Builder.Browse
{
    internal class BrowseBuilder : BuilderBase, IBrowseBuilder
    {
        public BrowseBuilder(ContextData contextData) : base(contextData, "browse")
        {
        }

        public IBrowseRecommendationsBuilder Recommendations => new BrowseRecommendationsBuilder(this.ContextData);

        public IBrowseFeaturedPlaylistsBuilder FeaturedPlaylists => new BrowseFeaturedPlaylistsBuilder(this.ContextData, this.RouteValuesPrefix);

        public IBrowseNewReleasesBuilder NewReleases => new BrowseNewReleasesBuilder(this.ContextData, this.RouteValuesPrefix);

        public IBrowseCategoriesBuilder Categories => Browse.Categories.Factory.CreateCategoriesBuilder(this.ContextData, this.RouteValuesPrefix);

        public IBrowseCategoryBuilder Category(string id)
        {
            return Browse.Categories.Factory.CreateCategoryBuilder(this.ContextData, this.RouteValuesPrefix, id);
        }
    }
}
