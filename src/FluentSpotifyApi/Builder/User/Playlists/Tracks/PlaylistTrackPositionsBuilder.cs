using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model;
using Newtonsoft.Json;

namespace FluentSpotifyApi.Builder.User.Playlists.Tracks
{
    internal class PlaylistTrackPositionsBuilder : SequenceBuilderBase<int>, IPlaylistTrackPositionsBuilder
    {
        public PlaylistTrackPositionsBuilder(ContextData contextData, IEnumerable<object> routeValuesPrefix, string endpointName, IEnumerable<int> positions) 
            : base(contextData, routeValuesPrefix, endpointName, positions)
        {
        }

        public Task<PlaylistSnapshot> RemoveAsync(string snapshotId, CancellationToken cancellationToken)
        {
            return this.SendAsync<PlaylistSnapshot, TrackPositions>(HttpMethod.Delete, new TrackPositions { Positions = this.Sequence.ToArray(), SnapshotId = snapshotId }, cancellationToken);
        }

        private class TrackPositions
        {
            [JsonProperty(PropertyName = "positions")]
            public int[] Positions { get; set; }

            [JsonProperty(PropertyName = "snapshot_id")]
            public string SnapshotId { get; set; }
        }
    }
}
