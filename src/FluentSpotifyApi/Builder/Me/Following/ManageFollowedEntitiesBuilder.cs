using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FluentSpotifyApi.Builder.Me.Following
{
    internal class ManageFollowedEntitiesBuilder : EntitiesBuilderBase, IManageFollowedEntitiesBuilder
    {
        private readonly string entityName;

        public ManageFollowedEntitiesBuilder(ContextData contextData, IEnumerable<object> routeValuesPrefix, string endpointName, string entityName, IEnumerable<string> ids) 
            : base(contextData, routeValuesPrefix, endpointName, ids)
        {
            this.entityName = entityName;
        }

        public Task FollowAsync(CancellationToken cancellationToken)
        {
            return this.SendAsync<object, IdsWrapper>(
                HttpMethod.Put,
                new IdsWrapper { Ids = this.Sequence.ToList() },
                cancellationToken,
                queryStringParameters: new { type = this.entityName });
        }

        public Task UnfollowAsync(CancellationToken cancellationToken)
        {
            return this.SendAsync<object, IdsWrapper>(
                HttpMethod.Delete,
                new IdsWrapper { Ids = this.Sequence.ToList() },
                cancellationToken,
                queryStringParameters: new { type = this.entityName });
        }

        public Task<bool[]> CheckAsync(CancellationToken cancellationToken)
        {
            return this.GetListAsync<bool[]>(
                cancellationToken,
                queryStringParameters: new { type = this.entityName },
                additionalRouteValues: new[] { "contains" });
        }

        private class IdsWrapper
        {
            [JsonProperty(PropertyName = "ids")]
            public IList<string> Ids { get; set; }
        }
    }
}
