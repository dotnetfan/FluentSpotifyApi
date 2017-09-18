using System;
using System.Net.Http;
using System.Threading.Tasks;
using FluentSpotifyApi.Core.Exceptions;
using FluentSpotifyApi.Core.Model;
using FluentSpotifyApi.Core.Model.Wrappers;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace FluentSpotifyApi.Core.Internal.Extensions
{
    internal static class HttpResponseMessageExtensions
    {
        private const string RegularErrorWrapperSchemaString =
            @"{
                'definitions': {
                    'errorDefinition': {
                        'type': 'object',
                        'properties': {
                            'status': {'type':'integer'},  
                            'message': {'type':'string'}
                        },
                    }
                },

              'type': 'object',
              'properties': {
                'error': { '$ref': '#/definitions/errorDefinition' },
              },
            }";

        private const string AuthenticationErrorWrapperSchemaString =
            @"{
              'type': 'object',
              'properties': {
                    'error': {'type':'string'},  
                    'error_description': {'type':'string'}              
               },
            }";

        private static readonly JSchema RegularErrorWrapperSchema = JSchema.Parse(RegularErrorWrapperSchemaString);

        private static readonly JSchema AuthenticationErrorWrapperSchema = JSchema.Parse(AuthenticationErrorWrapperSchemaString);

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
                    var payload = JObject.Parse(content);
                    if (payload.IsValid(RegularErrorWrapperSchema))
                    {
                        regularErrorMessage = payload.ToObject<RegularErrorMessage>();
                    }
                    else if (payload.IsValid(AuthenticationErrorWrapperSchema))
                    {
                        authenticationError = payload.ToObject<AuthenticationError>();
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
