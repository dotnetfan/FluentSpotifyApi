using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Messages;

namespace FluentSpotifyApi.Builder.Browse
{
    internal class BrowseCategoriesBuilder : BuilderBase, IBrowseCategoriesBuilder
    {
        public BrowseCategoriesBuilder(ContextData contextData, IEnumerable<object> routeValuesPrefix, string endpointName) : base(contextData, routeValuesPrefix, endpointName)
        {
        }

        public Task<CategoriesPageMessage> GetAsync(string country, string locale, int limit, int offset, CancellationToken cancellationToken)
        {
            return this.GetAsync<CategoriesPageMessage>(cancellationToken, queryStringParameters: new { limit, offset }, optionalQueryStringParameters: new { country, locale });
        }
    }
}
