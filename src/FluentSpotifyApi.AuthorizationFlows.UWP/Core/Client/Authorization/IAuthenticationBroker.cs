using System;
using System.Threading;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;

namespace FluentSpotifyApi.AuthorizationFlows.UWP.Core.Client.Authorization
{
    internal interface IAuthenticationBroker
    {
        Task<WebAuthenticationResult> AuthenticateAsync(Uri authenticationUrl, Uri callbackUrl, CancellationToken cancellationToken);
    }
}
