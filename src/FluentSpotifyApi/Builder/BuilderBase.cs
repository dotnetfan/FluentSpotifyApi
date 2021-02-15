using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Core.Utils;
using FluentSpotifyApi.Serialization;

namespace FluentSpotifyApi.Builder
{
    internal abstract class BuilderBase
    {
        private static readonly JsonSerializerOptions JsonOptions;

        private readonly ContextData contextData;

        private readonly IList<object> routeValues;

        static BuilderBase()
        {
            JsonOptions = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
            JsonOptions.Converters.Add(new EntityConverter());
        }

        public BuilderBase(BuilderBase parent)
            : this(parent, Enumerable.Empty<object>())
        {
        }

        public BuilderBase(BuilderBase parent, IEnumerable<object> routeValues)
        {
            SpotifyArgumentAssertUtils.ThrowIfNull(parent, nameof(parent));
            SpotifyArgumentAssertUtils.ThrowIfNull(routeValues, nameof(routeValues));

            this.contextData = parent.contextData;
            this.routeValues = parent.routeValues.Concat(routeValues).ToList();
        }

        private BuilderBase(ContextData contextData)
        {
            this.contextData = contextData;
            this.routeValues = Array.Empty<object>();
        }

        protected RootBuilder CreateRootBuilder() => new RootBuilder(this.contextData);

        protected async Task<TResult> GetAsync<TResult>(
            CancellationToken cancellationToken,
            IEnumerable<object> additionalRouteValues = null,
            object queryParams = null)
        {
            var relativeUri = await this.GetRelativeUriAsync(additionalRouteValues, queryParams, cancellationToken).ConfigureAwait(false);

            return await SpotifyHttpUtils.HandleTimeoutAsync<IFluentSpotifyClient, TResult>(
                async innerCt => await this.contextData.HttpClientFactory.CreateClient().GetFromJsonAsync<TResult>(relativeUri, JsonOptions, innerCt).ConfigureAwait(false),
                cancellationToken).ConfigureAwait(false);
        }

        protected async Task SendAsync(
            HttpMethod httpMethod,
            CancellationToken cancellationToken,
            IEnumerable<object> additionalRouteValues = null,
            object queryParams = null)
        {
            var relativeUri = await this.GetRelativeUriAsync(additionalRouteValues, queryParams, cancellationToken).ConfigureAwait(false);
            using (var request = new HttpRequestMessage(httpMethod, relativeUri))
            {
                await SpotifyHttpUtils.HandleTimeoutAsync<IFluentSpotifyClient>(
                    async innerCt => (await this.contextData.HttpClientFactory.CreateClient().SendAsync(request, innerCt).ConfigureAwait(false)).Dispose(),
                    cancellationToken).ConfigureAwait(false);
            }
        }

        protected async Task<TResult> SendBodyAsync<TRequestBody, TResult>(
            HttpMethod httpMethod,
            TRequestBody requestBody,
            CancellationToken cancellationToken,
            IEnumerable<object> additionalRouteValues = null,
            object queryParams = null)
        {
            var relativeUri = await this.GetRelativeUriAsync(additionalRouteValues, queryParams, cancellationToken).ConfigureAwait(false);
            var content = JsonContent.Create(requestBody, options: JsonOptions);

            using (var request = new HttpRequestMessage(httpMethod, relativeUri))
            {
                request.Content = content;

                return await SpotifyHttpUtils.HandleTimeoutAsync<IFluentSpotifyClient, TResult>(
                    async innerCt =>
                    {
                        using (var response = await this.contextData.HttpClientFactory.CreateClient().SendAsync(request, innerCt).ConfigureAwait(false))
                        {
                            return await response.Content.ReadFromJsonAsync<TResult>(JsonOptions, innerCt).ConfigureAwait(false);
                        }
                    },
                    cancellationToken).ConfigureAwait(false);
            }
        }

        protected async Task SendBodyAsync<TRequestBody>(
            HttpMethod httpMethod,
            TRequestBody requestBody,
            CancellationToken cancellationToken,
            IEnumerable<object> additionalRouteValues = null,
            object queryParams = null)
        {
            var relativeUri = await this.GetRelativeUriAsync(additionalRouteValues, queryParams, cancellationToken).ConfigureAwait(false);
            var content = JsonContent.Create(requestBody, options: JsonOptions);

            using (var request = new HttpRequestMessage(httpMethod, relativeUri))
            {
                request.Content = content;

                await SpotifyHttpUtils.HandleTimeoutAsync<IFluentSpotifyClient>(
                    async innerCt => (await this.contextData.HttpClientFactory.CreateClient().SendAsync(request, innerCt).ConfigureAwait(false)).Dispose(),
                    cancellationToken).ConfigureAwait(false);
            }
        }

        protected async Task SendJpegAsync(
            HttpMethod httpMethod,
            byte[] jpeg,
            CancellationToken cancellationToken,
            IEnumerable<object> additionalRouteValues = null,
            object queryParams = null)
        {
            var relativeUri = await this.GetRelativeUriAsync(additionalRouteValues, queryParams, cancellationToken).ConfigureAwait(false);
            var content = new StringContent(Convert.ToBase64String(jpeg));
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");

            using (var request = new HttpRequestMessage(httpMethod, relativeUri))
            {
                request.Content = content;

                await SpotifyHttpUtils.HandleTimeoutAsync<IFluentSpotifyClient>(
                    async innerCt => (await this.contextData.HttpClientFactory.CreateClient().SendAsync(request, innerCt).ConfigureAwait(false)).Dispose(),
                    cancellationToken).ConfigureAwait(false);
            }
        }

        private async Task<Uri> GetRelativeUriAsync(IEnumerable<object> additionalRouteValues, object queryParams, CancellationToken cancellationToken)
        {
            var routeValues = this.routeValues.Concat(additionalRouteValues ?? Enumerable.Empty<object>()).ToList();
            var destructuredQueryParams = this.DestructureQueryParamsObjectRecursively(queryParams).Where(item => item.Value != null).ToList();

            bool transformRouteValues = false;
            bool transformQueryParamters = false;
            if ((transformRouteValues = routeValues.Any(x => x is CurrentUserIdPlaceholder)) || (transformQueryParamters = destructuredQueryParams.Any(x => x.Value is CurrentUserIdPlaceholder)))
            {
                var currentUser = await this.contextData.CurrentUserProvider.GetAsync(cancellationToken).ConfigureAwait(false);

                if (transformRouteValues)
                {
                    routeValues = routeValues.Select(x => x is CurrentUserIdPlaceholder ? currentUser.Id : x).ToList();
                }

                if (transformQueryParamters)
                {
                    destructuredQueryParams = destructuredQueryParams
                        .Select(x => x.Value is CurrentUserIdPlaceholder ? new KeyValuePair<string, object>(x.Key, currentUser.Id) : x)
                        .ToList();
                }
            }

            var result = SpotifyUriUtils.GetRelativeUri(routeValues, destructuredQueryParams);
            return result;
        }

        private IEnumerable<KeyValuePair<string, object>> DestructureQueryParamsObjectRecursively(object value)
        {
            if (value == null)
            {
                yield break;
            }

            var type = value.GetType();

            foreach (var property in type.GetProperties())
            {
                var propertyValue = property.GetValue(value);

                if (typeof(IEnumerable<KeyValuePair<string, object>>).GetTypeInfo().IsAssignableFrom(property.PropertyType))
                {
                    foreach (var item in (IEnumerable<KeyValuePair<string, object>>)propertyValue)
                    {
                        yield return item;
                    }
                }
                else if (typeof(KeyValuePair<string, object>).GetTypeInfo().IsAssignableFrom(property.PropertyType))
                {
                    yield return (KeyValuePair<string, object>)propertyValue;
                }
                else if (property.PropertyType == typeof(string) ||
                        property.PropertyType.IsValueType ||
                        property.PropertyType == typeof(Uri) ||
                        property.PropertyType == typeof(CurrentUserIdPlaceholder))
                {
                    yield return new KeyValuePair<string, object>(property.Name, propertyValue);
                }
                else
                {
                    foreach (var item in this.DestructureQueryParamsObjectRecursively(propertyValue))
                    {
                        yield return item;
                    }
                }
            }
        }

        public sealed class RootBuilder : BuilderBase
        {
            public RootBuilder(ContextData contextData)
                : base(contextData)
            {
            }
        }
    }
}
