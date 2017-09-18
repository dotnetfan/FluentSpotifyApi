using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model;

namespace FluentSpotifyApi.Builder.Me.Library
{
    internal class GetLibraryEntitiesBuilder<T> : BuilderBase, IGetLibraryEntitiesBuilder<T>
    {
        public GetLibraryEntitiesBuilder(ContextData contextData, IEnumerable<object> routeValuesPrefix, string endpointName) : base(contextData, routeValuesPrefix, endpointName)
        {
        }

        public Task<Page<T>> GetAsync(int limit, int offset, string market, CancellationToken cancellationToken)
        {
            return this.GetAsync<Page<T>>(cancellationToken, queryStringParameters: new { limit, offset }, optionalQueryStringParameters: new { market });
        }
    }
}
