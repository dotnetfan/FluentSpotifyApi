using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Core.Utils;
using FluentSpotifyApi.Extensions;

namespace FluentSpotifyApi.Builder.Me.Following
{
    internal class FollowedItemsBuilder : BuilderBase, IFollowedItemsBuilder
    {
        protected readonly string itemType;

        public FollowedItemsBuilder(BuilderBase parent, string itemType)
            : base(parent, "following".Yield())
        {
            SpotifyArgumentAssertUtils.ThrowIfNullOrEmpty(itemType, nameof(itemType));

            this.itemType = itemType;
        }

        public Task FollowAsync(IEnumerable<string> ids, CancellationToken cancellationToken)
        {
            SpotifyArgumentAssertUtils.ThrowIfNull(ids, nameof(ids));

            return this.SendBodyAsync(
                HttpMethod.Put,
                new IdsRequest { Ids = ids.ToArray() },
                cancellationToken,
                queryParams: new { type = this.itemType });
        }

        public Task UnfollowAsync(IEnumerable<string> ids, CancellationToken cancellationToken)
        {
            SpotifyArgumentAssertUtils.ThrowIfNull(ids, nameof(ids));

            return this.SendBodyAsync(
                HttpMethod.Delete,
                new IdsRequest { Ids = ids.ToArray() },
                cancellationToken,
                queryParams: new { type = this.itemType });
        }

        public Task<bool[]> CheckAsync(IEnumerable<string> ids, CancellationToken cancellationToken)
        {
            SpotifyArgumentAssertUtils.ThrowIfNull(ids, nameof(ids));

            return this.GetAsync<bool[]>(cancellationToken, additionalRouteValues: new[] { "contains" }, queryParams: new { type = this.itemType, ids = ids.JoinWithComma() });
        }

        private class IdsRequest
        {
            [JsonPropertyName("ids")]
            public string[] Ids { get; set; }
        }
    }
}
