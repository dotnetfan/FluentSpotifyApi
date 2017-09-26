using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Messages;

namespace FluentSpotifyApi.Builder.Me.Player
{
    internal class DevicesBuilder : BuilderBase, IDevicesBuilder
    {
        public DevicesBuilder(ContextData contextData, IEnumerable<object> routeValuesPrefix, string endpointName) : base(contextData, routeValuesPrefix, endpointName)
        {
        }

        public Task<DevicesMessage> GetAsync(CancellationToken cancellationToken)
        {
            return this.GetAsync<DevicesMessage>(cancellationToken, additionalRouteValues: new[] { "devices" });
        }
    }
}
