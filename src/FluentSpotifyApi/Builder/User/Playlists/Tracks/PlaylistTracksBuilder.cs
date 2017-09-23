using System.Collections.Generic;
using System.Net.Http;
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
            return this.SendAsync<PlaylistSnapshot, ReoderParameters>(
                HttpMethod.Put,
                new ReoderParameters { RangeStart = rangeStart, InsertBefore = insertBefore, RangeLength = rangeLength, SnapshotId = snapshotId }, 
                cancellationToken);
        }

        private class ReoderParameters
        {
            [JsonProperty(PropertyName = "range_start")]
            public int RangeStart { get; set; }

            [JsonProperty(PropertyName = "insert_before")]
            public int InsertBefore { get; set; }

            [JsonProperty(PropertyName = "range_length")]
            public int RangeLength { get; set; }

            [JsonProperty(PropertyName = "snapshot_id", NullValueHandling = NullValueHandling.Ignore)]
            public string SnapshotId { get; set; }
        }
    }
}
