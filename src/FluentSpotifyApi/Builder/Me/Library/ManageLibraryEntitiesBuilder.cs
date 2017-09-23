using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FluentSpotifyApi.Builder.Me.Library
{
    internal class ManageLibraryEntitiesBuilder : EntitiesBuilderBase, IManageLibraryEntitiesBuilder
    {
        public ManageLibraryEntitiesBuilder(ContextData contextData, IEnumerable<object> routeValuesPrefix, string endpointName, IEnumerable<string> ids) 
            : base(contextData, routeValuesPrefix, endpointName, ids)
        {
        }

        public Task SaveAsync(CancellationToken cancellationToken)
        {
            return this.SendAsync<object, IdsWrapper>(HttpMethod.Put, new IdsWrapper { Ids = this.Sequence.ToList() }, cancellationToken);
        }

        public Task RemoveAsync(CancellationToken cancellationToken)
        {
            return this.SendAsync<object, IdsWrapper>(HttpMethod.Delete, new IdsWrapper { Ids = this.Sequence.ToList() }, cancellationToken);
        }

        public Task<bool[]> CheckAsync(CancellationToken cancellationToken)
        {
            return this.GetListAsync<bool[]>(cancellationToken, additionalRouteValues: new[] { "contains" });
        }

        private class IdsWrapper
        {
            [JsonProperty(PropertyName = "ids")]
            public IList<string> Ids { get; set; }
        }
    }
}
