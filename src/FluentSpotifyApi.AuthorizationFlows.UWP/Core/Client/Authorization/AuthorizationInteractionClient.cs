using System;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.Authorization;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.Authorization.Exceptions;
using FluentSpotifyApi.AuthorizationFlows.UWP.Core.Client.Authorization.Exceptions;
using FluentSpotifyApi.AuthorizationFlows.UWP.Extensions;
using Windows.Security.Authentication.Web;

namespace FluentSpotifyApi.AuthorizationFlows.UWP.Core.Client.Authorization
{
    internal class AuthorizationInteractionClient : IAuthorizationInteractionClient
    {
        public async Task<Uri> AuthorizeAsync(Uri authorizationUri, Uri redirectUri, CancellationToken cancellationToken)
        {
            var dispatcher = Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher;

            var result = await dispatcher.RunDirectlyOrDispatchAsync(
                async innerCt =>
                {
                    try
                    {
                        return await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, authorizationUri, redirectUri).AsTask(cancellationToken).ConfigureAwait(false);
                    }
                    catch (Exception e)
                    {
                        throw new SpotifyAuthorizationException("An error has occurred while communicating with Spotify Accounts Service. See inner exception for details.", e);
                    }
                },
                cancellationToken).ConfigureAwait(false);

            switch (result.ResponseStatus)
            {
                case WebAuthenticationStatus.Success:
                    try
                    {
                        return new Uri(result.ResponseData);
                    }
                    catch (Exception e)
                    {
                        throw new SpotifyAuthorizationException("An invalid URL string has been returned from the Spotify Accounts Service. See inner exception for details.", e);
                    }

                default:
                    throw new SpotifyWebAuthenticationBrokerException(result.ResponseStatus);
            }
        }
    }
}
