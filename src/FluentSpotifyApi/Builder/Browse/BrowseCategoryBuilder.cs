using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Browse;

namespace FluentSpotifyApi.Builder.Browse
{
    internal class BrowseCategoryBuilder : EntityBuilderBase, IBrowseCategoryBuilder
    {
        public BrowseCategoryBuilder(BuilderBase parent, string id)
            : base(parent, "categories", id)
        {
        }

        public IBrowseCategoryPlaylistsBuilder Playlists => new BrowseCategoryPlaylistsBuilder(this);

        public Task<Category> GetAsync(string country, string locale, CancellationToken cancellationToken)
        {
            return this.GetAsync<Category>(cancellationToken, queryParams: new { country, locale });
        }
    }
}
