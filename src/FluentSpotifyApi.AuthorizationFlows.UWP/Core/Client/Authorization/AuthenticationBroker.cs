using System;
using System.Threading;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;

namespace FluentSpotifyApi.AuthorizationFlows.UWP.Core.Client.Authorization
{
    internal class AuthenticationBroker : IAuthenticationBroker
    {
        public Task<WebAuthenticationResult> AuthenticateAsync(Uri authenticationUrl, Uri callbackUrl, CancellationToken cancellationToken)
        {
            return WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, authenticationUrl, callbackUrl).AsTask(cancellationToken);
        }
    }
}
