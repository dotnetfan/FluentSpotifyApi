using System;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.Authorization;
using FluentSpotifyApi.AuthorizationFlows.Exceptions;
using FluentSpotifyApi.AuthorizationFlows.UWP.Exceptions;
using FluentSpotifyApi.AuthorizationFlows.UWP.Extensions;
using Windows.Security.Authentication.Web;

namespace FluentSpotifyApi.AuthorizationFlows.UWP.Core.Client.Authorization
{
    internal abstract class AuthorizationInteractionServiceBase<T> : IAuthorizationInteractionClient<T>
    {
        private readonly IAuthenticationBroker authenticationBroker;

        public AuthorizationInteractionServiceBase(IAuthenticationBroker authenticationBroker)
        {
            this.authenticationBroker = authenticationBroker;
        }

        public async Task<AuthorizationResponse<T>> AuthorizeAsync(Uri authorizationUrl, Uri callbackUrl, CancellationToken cancellationToken)
        {
            var dispatcher = Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher;

            var result = await dispatcher.RunDirectlyOrDispatchAsyncAsync(
                async innerCt =>
                {
                    try
                    {
                        return await this.authenticationBroker.AuthenticateAsync(authorizationUrl, callbackUrl, innerCt).ConfigureAwait(false);
                    }
                    catch (Exception e)
                    {
                        throw new SpotifyAuthorizationException("An unknown authorization error has occurred.", e);
                    }
                },
                cancellationToken).ConfigureAwait(false);

            switch (result.ResponseStatus)
            {
                case WebAuthenticationStatus.Success:
                    return this.GetAuthorizationReponseFromResponseData(result.ResponseData);
                default:
                    throw new SpotifyUwpAuthorizationException(result.ResponseStatus);
            }
        }

        protected abstract AuthorizationResponse<T> GetAuthorizationReponseFromResponseData(string responseData); 
    }
}
