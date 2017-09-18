using System.Collections.Generic;
using FluentSpotifyApi.Core.Internal.Extensions;

namespace FluentSpotifyApi.Builder
{
    internal class EntityBuilderBase : BuilderBase
    {
        protected readonly string Id;

        protected EntityBuilderBase(ContextData contextData, string endpointName, string id) 
            : this(contextData, null, endpointName, id)
        {
        }

        protected EntityBuilderBase(ContextData contextData, IEnumerable<object> routeValuesPrefix, string endpointName, string id) 
            : base(contextData, routeValuesPrefix, endpointName, id.Yield())
        {
            this.Id = id;
        }
    }
}
