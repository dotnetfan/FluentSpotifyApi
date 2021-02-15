using System.Threading;
using System.Threading.Tasks;

namespace FluentSpotifyApi.Builder.Me.Following
{
    internal class RetrievableFollowedItemsBuilder<T> : FollowedItemsBuilder, IRetrievableFollowedItemsBuilder<T>
    {
        public RetrievableFollowedItemsBuilder(BuilderBase parent, string itemType)
            : base(parent, itemType)
        {
        }

        public Task<T> GetAsync(string after, int? limit, CancellationToken cancellationToken)
        {
            return this.GetAsync<T>(cancellationToken, queryParams: new { type = this.itemType, after, limit });
        }
    }
}
