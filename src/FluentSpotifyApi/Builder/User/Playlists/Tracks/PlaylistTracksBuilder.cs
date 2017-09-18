using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model;
using Newtonsoft.Json;

namespace FluentSpotifyApi.Builder.User.Playlists.Tracks
{
    internal class PlaylistTracksBuilder : BuilderBase, IPlaylistTracksBuilder
    {
        public PlaylistTracksBuilder(ContextData contextData, IEnumerable<object> routeValuesPrefix, string endpointName) : base(contextData, routeValuesPrefix, endpointName)
        {
        }

        public Task<Page<PlaylistTrack>> GetAsync(string fields, int limit, int offset, string market, CancellationToken cancellationToken)
        {
            return this.GetAsync<Page<PlaylistTrack>>(cancellationToken, queryStringParameters: new { limit, offset }, optionalQueryStringParameters: new { fields, market });
        }

        public Task<PlaylistSnapshot> ReorderAsync(int rangeStart, int insertBefore, int rangeLength, string snapshotId, CancellationToken cancellationToken)
        {
            return this.PutAsync<PlaylistSnapshot, ReoderParameters>(
                new ReoderParameters(rangeStart, insertBefore) { RangeLength = rangeLength, SnapshotId = snapshotId }, 
                cancellationToken);
        }

        private class ReoderParameters
        {
            public ReoderParameters(int rangeStart, int insertBefore)
            {
                this.RangeStart = rangeStart;
                this.InsertBefore = insertBefore;
            }

            [JsonProperty(PropertyName = "range_start")]
            public int RangeStart { get; private set; }

            [JsonProperty(PropertyName = "insert_before")]
            public int InsertBefore { get; private set; }

            [JsonProperty(PropertyName = "range_length", NullValueHandling = NullValueHandling.Ignore)]
            public int? RangeLength { get; set; }

            [JsonProperty(PropertyName = "snapshot_id", NullValueHandling = NullValueHandling.Ignore)]
            public string SnapshotId { get; set; }
        }
    }
}
