using System;
using System.Net.Http;
using System.Threading.Tasks;
using FluentSpotifyApi.Core.Exceptions;
using FluentSpotifyApi.Core.Model;
using FluentSpotifyApi.Core.Model.Wrappers;
using Newtonsoft.Json;

namespace FluentSpotifyApi.Core.Internal.Extensions
{
    internal static class HttpResponseMessageExtensions
    {
        public static async Task EnsureSuccessStatusCodeAsync(this HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return;
            }

            string content = string.Empty;
            RegularErrorMessage regularErrorMessage = null;
            AuthenticationError authenticationError = null;
            if (response.Content != null)
            {
                try
                {
                    content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    try
                    {
                        regularErrorMessage = JsonConvert.DeserializeObject<RegularErrorMessage>(content);
                    }
                    catch (Exception)
                    {
                        authenticationError = JsonConvert.DeserializeObject<AuthenticationError>(content);
                    }                    
                }
                catch (Exception)
                {
                }
            }

            if (regularErrorMessage != null)
            {
                throw new SpotifyHttpResponseWithRegularErrorException(response.StatusCode, response.Headers, content, regularErrorMessage.Error);
            }
            else if (authenticationError != null)
            {
                throw new SpotifyHttpResponseWithAuthenticationErrorException(response.StatusCode, response.Headers, content, authenticationError);
            }
            else
            {
                throw new SpotifyHttpResponseWithErrorCodeException(response.StatusCode, response.Headers, content);
            }
        }
    }
}
