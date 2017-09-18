using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FluentSpotifyApi.Builder
{
    internal class EntitiesBuilderBase : SequenceBuilderBase<string>
    {
        public EntitiesBuilderBase(ContextData contextData, string enpointName, IEnumerable<string> ids) 
            : base(contextData, null, enpointName, ids)
        {
        }

        public EntitiesBuilderBase(ContextData contextData, IEnumerable<object> routeValuesPrefix, string endpointName, IEnumerable<string> ids) 
            : base(contextData, routeValuesPrefix, endpointName, ids)
        {
        }

        protected Task<T> GetListAsync<T>(
            CancellationToken cancellationToken, 
            object queryStringParameters = null,
            object optionalQueryStringParameters = null, 
            IEnumerable<KeyValuePair<string, string>> requestHeaders = null, 
            params object[] additionalRouteValues)
        {
            return this.GetAsync<T>(
                cancellationToken,
                queryStringParameters: new { ids = string.Join(",", this.Sequence), originalParameters = queryStringParameters },
                optionalQueryStringParameters: optionalQueryStringParameters,
                requestHeaders: requestHeaders,
                additionalRouteValues: additionalRouteValues);
        }
    }
}
