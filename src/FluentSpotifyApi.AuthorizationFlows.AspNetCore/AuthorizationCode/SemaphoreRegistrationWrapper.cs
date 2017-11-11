using System.Threading;
using FluentSpotifyApi.Core.Internal;

namespace FluentSpotifyApi.AuthorizationFlows.AspNetCore.AuthorizationCode
{
    internal class SemaphoreRegistrationWrapper : Wrapper<SemaphoreSlim>
    {
        public SemaphoreRegistrationWrapper(SemaphoreSlim value, bool isOwned) : base(value, isOwned)
        {
        }
    }
}
