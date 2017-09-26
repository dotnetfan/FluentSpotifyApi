using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Core.Client;
using FluentSpotifyApi.Core.Internal;
using FluentSpotifyApi.Core.Internal.Extensions;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.Builder
{
    internal class BuilderBase
    {
        protected static readonly ITransformer UserTransformer = new Transformer<IUser>(user => user.Id);

        protected readonly ContextData ContextData;

        protected readonly object[] RouteValuesPrefix;

        public BuilderBase(ContextData contextData, IEnumerable<object> routeValuesPrefix, string endpointName) : this(contextData, routeValuesPrefix, endpointName, null)
        {
        }

        public BuilderBase(ContextData contextData, string endpointName) : this(contextData, null, endpointName, null)
        {
        }

        public BuilderBase(ContextData contextData, IEnumerable<object> routeValuesPrefix, string endpointName, IEnumerable<object> routeValuesSuffix)
        {
            this.ContextData = contextData;
            this.RouteValuesPrefix = routeValuesPrefix.EmptyIfNull().Concat(endpointName.Yield()).Concat(routeValuesSuffix.EmptyIfNull()).ToArray();
        }

        protected Task<TResult> GetAsync<TResult>(
            CancellationToken cancellationToken, 
            object queryStringParameters = null, 
            object optionalQueryStringParameters = null, 
            IEnumerable<object> additionalRouteValues = null)
        {
            return this.SendAsync<TResult>(
                HttpMethod.Get, 
                cancellationToken, 
                queryStringParameters, 
                optionalQueryStringParameters, 
                additionalRouteValues);
        }

        protected Task<TResult> SendAsync<TResult>(
            HttpMethod httpMethod, 
            CancellationToken cancellationToken, 
            object queryStringParameters = null, 
            object optionalQueryStringParameters = null,
            IEnumerable<object> additionalRouteValues = null)
        {
            return this.ContextData.SpotifyHttpClient.SendAsync<TResult>(
                this.GetUriParts(queryStringParameters, optionalQueryStringParameters, additionalRouteValues),
                httpMethod,                
                null,
                null,
                cancellationToken);
        }

        protected Task<TResult> SendAsync<TResult, TRequestBody>(
            HttpMethod httpMethod, 
            TRequestBody requestBody, 
            CancellationToken cancellationToken, 
            object queryStringParameters = null, 
            object optionalQueryStringParameters = null, 
            IEnumerable<object> additionalRouteValues = null)
        {
            return this.ContextData.SpotifyHttpClient.SendWithJsonBodyAsync<TResult, TRequestBody>(
                this.GetUriParts(queryStringParameters, optionalQueryStringParameters, additionalRouteValues),                
                httpMethod,                                                
                null,
                requestBody,
                cancellationToken);
        }

        protected Task<TResult> SendAsync<TResult>(
            HttpMethod httpMethod,
            Func<CancellationToken, Task<Stream>> streamProvider,
            string streamContentType,
            CancellationToken cancellationToken,
            object queryStringParameters = null,
            object optionalQueryStringParameters = null,
            IEnumerable<object> additionalRouteValues = null)
        {
            return this.ContextData.SpotifyHttpClient.SendWithStreamBodyAsync<TResult>(
                this.GetUriParts(queryStringParameters, optionalQueryStringParameters, additionalRouteValues),
                httpMethod,
                null,
                streamProvider,
                streamContentType,
                cancellationToken);
        }

        private static object CombineParameters(object parameters, object optionalParameters)
        {
            var optionalPropertyBag = SpotifyObjectHelpers.GetPropertyBag(optionalParameters).Where(item => item.Value != null).ToList();

            if (optionalPropertyBag.Any())
            {
                return new { parameters = parameters, optionalParameters = optionalPropertyBag };
            }
            else
            {
                return parameters;
            }
        }

        private object[] CombineRouteValues(IEnumerable<object> additionalRouteValues)
        {
            return this.RouteValuesPrefix.Concat(additionalRouteValues.EmptyIfNull()).ToArray();
        }

        private UriParts GetUriParts(object queryStringParameters, object optionalQueryStringParameters, IEnumerable<object> additionalRouteValues)
        {
            return new UriParts
            {
                BaseUri = this.ContextData.FluentSpotifyClientOptionsProvider.Get().WebApiEndpoint,
                QueryStringParameters = CombineParameters(queryStringParameters, optionalQueryStringParameters),
                RouteValues = this.CombineRouteValues(additionalRouteValues),
            };
        }
    }
}
