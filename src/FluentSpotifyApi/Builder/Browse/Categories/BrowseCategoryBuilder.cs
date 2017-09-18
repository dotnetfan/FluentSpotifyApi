using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Browse;

namespace FluentSpotifyApi.Builder.Browse.Categories
{
    internal class BrowseCategoryBuilder : EntityBuilderBase, IBrowseCategoryBuilder
    {
        public BrowseCategoryBuilder(ContextData contextData, IEnumerable<object> routeValuesPrefix, string endpointName, string id) 
            : base(contextData, routeValuesPrefix, endpointName, id)
        {
        }

        public IBrowseCategoryPlaylistsBuilder Playlists => new BrowseCategoryPlaylistsBuilder(ContextData, RouteValuesPrefix);

        public Task<Category> GetAsync(string country, string locale, CancellationToken cancellationToken)
        {
            return this.GetAsync<Category>(cancellationToken, optionalQueryStringParameters: new { country, locale });
        }
    }
}
