using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Extensions;
using FluentSpotifyApi.Model.Browse;

namespace FluentSpotifyApi.Builder.Browse
{
    internal class BrowseCategoriesBuilder : BuilderBase, IBrowseCategoriesBuilder
    {
        public BrowseCategoriesBuilder(BuilderBase parent)
            : base(parent, "categories".Yield())
        {
        }

        public Task<CategoriesPageResponse> GetAsync(string country, string locale, int? limit, int? offset, CancellationToken cancellationToken)
        {
            return this.GetAsync<CategoriesPageResponse>(cancellationToken, queryParams: new { country, locale, limit, offset });
        }
    }
}
