using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FluentSpotifyApi.Builder.Me.Following
{
    internal class GetFollowedEntitiesBuilder<T> : BuilderBase, IGetFollowedEntitiesBuilder<T>
    {
        private readonly string entityName;

        public GetFollowedEntitiesBuilder(ContextData contextData, IEnumerable<object> routeValuesPrefix, string endpointName, string entityName) 
            : base(contextData, routeValuesPrefix, endpointName)
        {
            this.entityName = entityName;
        }

        public Task<T> GetAsync(int limit, string after, CancellationToken cancellationToken)
        {
            return this.GetAsync<T>(cancellationToken, queryStringParameters: new { type = this.entityName, limit }, optionalQueryStringParameters: new { after });
        }
    }
}
