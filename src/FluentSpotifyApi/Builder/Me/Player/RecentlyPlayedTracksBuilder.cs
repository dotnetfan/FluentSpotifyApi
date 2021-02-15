using System;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Extensions;
using FluentSpotifyApi.Model;
using FluentSpotifyApi.Model.Player;

namespace FluentSpotifyApi.Builder.Me.Player
{
    internal class RecentlyPlayedTracksBuilder : BuilderBase, IRecentlyPlayedTracksBuilder
    {
        public RecentlyPlayedTracksBuilder(BuilderBase parent)
            : base(parent, "recently-played".Yield())
        {
        }

        public Task<CursorBasedPage<PlayHistory>> GetAsync(int? limit, DateTime? after, DateTime? before, CancellationToken cancellationToken)
        {
            return this.GetAsync<CursorBasedPage<PlayHistory>>(
                cancellationToken,
                queryParams: new
                {
                    limit,
                    after = after?.ToTimestampMilliseconds(),
                    before = before?.ToTimestampMilliseconds()
                });
        }
    }
}
