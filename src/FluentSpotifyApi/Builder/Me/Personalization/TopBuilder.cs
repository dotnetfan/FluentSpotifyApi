using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Core.Utils;
using FluentSpotifyApi.Extensions;
using FluentSpotifyApi.Model;

namespace FluentSpotifyApi.Builder.Me.Personalization
{
    internal class TopBuilder<T> : BuilderBase, ITopBuilder<T>
    {
        public TopBuilder(BuilderBase parent, string entityName)
            : base(parent, new[] { "top", entityName })
        {
            SpotifyArgumentAssertUtils.ThrowIfNullOrEmpty(entityName, nameof(entityName));
        }

        public Task<Page<T>> GetAsync(TimeRange? timeRange, int? limit, int? offset, CancellationToken cancellationToken)
        {
            return this.GetAsync<Page<T>>(
                cancellationToken,
                queryParams: new { time_range = timeRange?.GetEnumMemberValue(), limit, offset });
        }
    }
}
