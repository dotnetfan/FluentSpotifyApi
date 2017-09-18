using System;
using System.Linq;
using FluentSpotifyApi.AuthorizationFlows.Core.Client.Authorization;
using FluentSpotifyApi.AuthorizationFlows.Exceptions;
using Windows.Foundation;

namespace FluentSpotifyApi.AuthorizationFlows.UWP.Core.Client.Authorization
{
    internal class AuthorizationCodeAuthorizationInteractionClient : AuthorizationInteractionServiceBase<string>
    {
        public AuthorizationCodeAuthorizationInteractionClient(IAuthenticationBroker authenticationBroker) : base(authenticationBroker) 
        {
        }

        protected override AuthorizationResponse<string> GetAuthorizationReponseFromResponseData(string responseData)
        {
            WwwFormUrlDecoder decoder;
            try
            {
                var responseUrl = new Uri(responseData);
                decoder = new WwwFormUrlDecoder(responseUrl.Query);
            }
            catch (Exception e)
            {
                throw new SpotifyAuthorizationException("An invalid URL string has been returned from the Spotify Accounts Service.", e);
            }

            var code = decoder.FirstOrDefault(item => item.Name == "code")?.Value;
 
            if (string.IsNullOrEmpty(code))
            {
                throw new SpotifyAuthorizationException("An invalid authorization code has been returned from the Spotify Accounts Service.");
            }

            return new AuthorizationResponse<string>(code, decoder.FirstOrDefault(item => item.Name == "state")?.Value);
        }
    }
}
