using System.Threading;

namespace FluentSpotifyApi.AuthorizationFlows.AspNetCore.AuthorizationCode
{
    internal interface ISemaphoreProvider
    {
        SemaphoreSlim Get();
    }
}
