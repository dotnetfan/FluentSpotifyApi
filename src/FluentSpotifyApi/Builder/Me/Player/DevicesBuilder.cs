using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Extensions;
using FluentSpotifyApi.Model.Player;

namespace FluentSpotifyApi.Builder.Me.Player
{
    internal class DevicesBuilder : BuilderBase, IDevicesBuilder
    {
        public DevicesBuilder(BuilderBase parent)
            : base(parent, "devices".Yield())
        {
        }

        public Task<DevicesResponse> GetAsync(CancellationToken cancellationToken)
        {
            return this.GetAsync<DevicesResponse>(cancellationToken);
        }
    }
}
