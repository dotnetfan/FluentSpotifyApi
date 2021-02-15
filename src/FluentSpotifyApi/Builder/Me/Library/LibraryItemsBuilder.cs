using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Core.Utils;
using FluentSpotifyApi.Extensions;
using FluentSpotifyApi.Model;

namespace FluentSpotifyApi.Builder.Me.Library
{
    internal class LibraryItemsBuilder<T> : BuilderBase, ILibraryItemsBuilder<T>
    {
        public LibraryItemsBuilder(BuilderBase parent, string itemType)
            : base(parent, itemType.Yield())
        {
            SpotifyArgumentAssertUtils.ThrowIfNullOrEmpty(itemType, nameof(itemType));
        }

        public Task<Page<T>> GetAsync(int? limit, int? offset, string market, CancellationToken cancellationToken)
        {
            return this.GetAsync<Page<T>>(cancellationToken, queryParams: new { limit, offset, market });
        }

        public Task SaveAsync(IEnumerable<string> ids, CancellationToken cancellationToken)
        {
            SpotifyArgumentAssertUtils.ThrowIfNull(ids, nameof(ids));

            return this.SendBodyAsync(HttpMethod.Put, new IdsRequest { Ids = ids.ToArray() }, cancellationToken);
        }

        public Task RemoveAsync(IEnumerable<string> ids, CancellationToken cancellationToken)
        {
            SpotifyArgumentAssertUtils.ThrowIfNull(ids, nameof(ids));

            return this.SendBodyAsync(HttpMethod.Delete, new IdsRequest { Ids = ids.ToArray() }, cancellationToken);
        }

        public Task<bool[]> CheckAsync(IEnumerable<string> ids, CancellationToken cancellationToken)
        {
            SpotifyArgumentAssertUtils.ThrowIfNull(ids, nameof(ids));

            return this.GetAsync<bool[]>(cancellationToken, additionalRouteValues: new[] { "contains" }, queryParams: new { ids = ids.JoinWithComma() });
        }

        private class IdsRequest
        {
            [JsonPropertyName("ids")]
            public string[] Ids { get; set; }
        }
    }
}
