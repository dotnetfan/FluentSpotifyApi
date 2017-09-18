using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Core.Internal.Extensions;
using FluentSpotifyApi.Model;

namespace FluentSpotifyApi.Builder.Me.Personalization.Top
{
    internal class TopBuilder<T> : BuilderBase, ITopBuilder<T>
    {
        public TopBuilder(ContextData contextData, IEnumerable<object> routeValuesPrefix, string endpointName) : base(contextData, routeValuesPrefix.Concat("top".Yield()), endpointName)
        {
        }

        public Task<Page<T>> GetAsync(int limit, int offset, TimeRange timeRange, CancellationToken cancellationToken)
        {
            return this.GetAsync<Page<T>>(
                cancellationToken, 
                queryStringParameters: new { limit, offset, time_range = timeRange.GetDescription() });
        }
    }
}
