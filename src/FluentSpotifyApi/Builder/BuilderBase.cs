using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
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

        protected Task<T> GetAsync<T>(
            CancellationToken cancellationToken, 
            object queryStringParameters = null, 
            object optionalQueryStringParameters = null, 
            IEnumerable<KeyValuePair<string, string>> requestHeaders = null,
            params object[] additionalRouteValues)
        {
            return this.SendAsync<T>(
                HttpMethod.Get, 
                cancellationToken, 
                queryStringParameters, 
                optionalQueryStringParameters, 
                requestHeaders, 
                additionalRouteValues);
        }

        protected Task<TResult> PostAsync<TResult, TRequestBody>(
            TRequestBody requestBody, 
            CancellationToken cancellationToken, 
            object queryStringParameters = null, 
            object optionalQueryStringParameters = null, 
            IEnumerable<KeyValuePair<string, string>> requestHeaders = null, 
            params object[] additionalRouteValues)
        {
            return this.SendAsync<TResult, TRequestBody>(
                HttpMethod.Post, 
                requestBody, 
                cancellationToken, 
                queryStringParameters, 
                optionalQueryStringParameters, 
                requestHeaders, 
                additionalRouteValues);
        }

        protected Task<T> PutAsync<T>(
            CancellationToken cancellationToken, 
            object queryStringParameters = null, 
            object optionalQueryStringParameters = null, 
            IEnumerable<KeyValuePair<string, string>> requestHeaders = null, 
            params object[] additionalRouteValues)
        {
            return this.SendAsync<T>(
                HttpMethod.Put, 
                cancellationToken, 
                queryStringParameters, 
                optionalQueryStringParameters, 
                requestHeaders, 
                additionalRouteValues);
        }

        protected Task<TResult> PutAsync<TResult, TRequestBody>(
            TRequestBody requestBody, 
            CancellationToken cancellationToken, 
            object queryStringParameters = null, 
            object optionalQueryStringParameters = null, 
            IEnumerable<KeyValuePair<string, string>> requestHeaders = null, 
            params object[] additionalRouteValues)
        {
            return this.SendAsync<TResult, TRequestBody>(
                HttpMethod.Put, 
                requestBody, 
                cancellationToken,
                queryStringParameters, 
                optionalQueryStringParameters, 
                requestHeaders, 
                additionalRouteValues);
        }

        protected Task<T> DeleteAsync<T>(
            CancellationToken cancellationToken, 
            object queryStringParameters = null, 
            object optionalQueryStringParameters = null, 
            IEnumerable<KeyValuePair<string, string>> requestHeaders = null, 
            params object[] additionalRouteValues)
        {
            return this.SendAsync<T>(
                HttpMethod.Delete, 
                cancellationToken, 
                queryStringParameters, 
                optionalQueryStringParameters, 
                requestHeaders, 
                additionalRouteValues);
        }

        protected Task<TResult> DeleteAsync<TResult, TRequestBody>(
            TRequestBody requestBody, 
            CancellationToken cancellationToken, 
            object queryStringParameters = null, 
            object optionalQueryStringParameters = null, 
            IEnumerable<KeyValuePair<string, string>> requestHeaders = null, 
            params object[] additionalRouteValues)
        {
            return this.SendAsync<TResult, TRequestBody>(
                HttpMethod.Delete, 
                requestBody,
                cancellationToken, 
                queryStringParameters, 
                optionalQueryStringParameters, 
                requestHeaders, 
                additionalRouteValues);
        }

        protected Task<T> SendAsync<T>(
            HttpMethod httpMethod, 
            CancellationToken cancellationToken, 
            object queryStringParameters = null, 
            object optionalQueryStringParameters = null,
            IEnumerable<KeyValuePair<string, string>> requestHeaders = null, 
            params object[] additionalRouteValues)
        {
            return this.ContextData.SpotifyHttpClient.SendAsync<T>(
                this.ContextData.FluentSpotifyClientOptionsProvider.Get().WebApiEndpoint,
                httpMethod,
                CombineParameters(queryStringParameters, optionalQueryStringParameters),
                null, 
                requestHeaders,
                cancellationToken,
                this.CombineRouteValues(additionalRouteValues));
        }

        protected Task<TResult> SendAsync<TResult, TRequestBody>(
            HttpMethod httpMethod, 
            TRequestBody requestBody, 
            CancellationToken cancellationToken, 
            object queryStringParameters = null, 
            object optionalQueryStringParameters = null, 
            IEnumerable<KeyValuePair<string, string>> requestHeaders = null, 
            params object[] additionalRouteValues)
        {
            return this.ContextData.SpotifyHttpClient.SendWithJsonBodyAsync<TResult, TRequestBody>(
                this.ContextData.FluentSpotifyClientOptionsProvider.Get().WebApiEndpoint,
                httpMethod,                 
                CombineParameters(queryStringParameters, optionalQueryStringParameters),
                requestBody,
                requestHeaders,
                cancellationToken,
                this.CombineRouteValues(additionalRouteValues));
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

        private object[] CombineRouteValues(object[] additionalRouteValues)
        {
            return this.RouteValuesPrefix.Concat(additionalRouteValues.EmptyIfNull()).ToArray();
        }
    }
}
