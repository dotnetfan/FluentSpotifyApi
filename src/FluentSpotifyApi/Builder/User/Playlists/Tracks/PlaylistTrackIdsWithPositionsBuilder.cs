using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model;

namespace FluentSpotifyApi.Builder.User.Playlists.Tracks
{
    [SuppressMessage("Microsoft.StyleCop.CSharp.SpacingRules", "SA1009:ClosingParenthesisMustBeSpacedCorrectly", Justification = "C# 7 Tuples")]
    internal class PlaylistTrackIdsWithPositionsBuilder : PlaylistTrackSequenceBuilderBase<(string Id, int[] Positions)>, IPlaylistTrackIdsWithPositionsBuilder
    {
        public PlaylistTrackIdsWithPositionsBuilder(
            ContextData contextData, 
            IEnumerable<object> routeValuesPrefix, 
            string endpointName, 
            IEnumerable<(string Id, int[] Positions)> idsWithPositions) 
            : base(contextData, routeValuesPrefix, endpointName, idsWithPositions)
        {
        }

        public Task<PlaylistSnapshot> RemoveAsync(string snapshotId, CancellationToken cancellationToken)
        {
            return this.SendAsync<PlaylistSnapshot, TrackUrlWithPositions>(
                HttpMethod.Delete,
                new TrackUrlWithPositions
                {
                    Tracks = this.Sequence.Select(item => new TrackUrlWithPosition { Uri = GetTrackUrl(item.Id), Positions = item.Positions }).ToArray(),
                    SnapshotId = snapshotId
                },
                cancellationToken);
        }
    }
}
