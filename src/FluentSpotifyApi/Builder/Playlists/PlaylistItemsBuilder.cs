using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Core.Utils;
using FluentSpotifyApi.Expressions.Fields;
using FluentSpotifyApi.Extensions;
using FluentSpotifyApi.Model;
using FluentSpotifyApi.Model.Playlists;

namespace FluentSpotifyApi.Builder.Playlists
{
    internal class PlaylistItemsBuilder : BuilderBase, IPlaylistItemsBuilder
    {
        public PlaylistItemsBuilder(BuilderBase parent)
            : base(parent, "tracks".Yield())
        {
        }

        public Task<Page<PlaylistTrack>> GetAsync(Action<IFieldsBuilder<Page<PlaylistTrack>>> buildFields, string market, int? limit, int? offset, CancellationToken cancellationToken)
        {
            return this.GetAsync(FieldsProvider.Get(buildFields), market, limit, offset, cancellationToken);
        }

        public Task<Page<PlaylistTrack>> GetAsync(string fields, string market, int? limit, int? offset, CancellationToken cancellationToken)
        {
            return this.GetAsync<Page<PlaylistTrack>>(cancellationToken, queryParams: new { fields, market, limit, offset });
        }

        public Task<PlaylistSnapshot> AddAsync(IEnumerable<string> uris, int? position, CancellationToken cancellationToken)
        {
            SpotifyArgumentAssertUtils.ThrowIfNull(uris, nameof(uris));

            return this.SendBodyAsync<AddUrisRequest, PlaylistSnapshot>(
                HttpMethod.Post,
                new AddUrisRequest { Uris = uris.ToArray(), Position = position },
                cancellationToken);
        }

        public Task<PlaylistSnapshot> RemoveAsync(IEnumerable<string> uris, CancellationToken cancellationToken)
        {
            SpotifyArgumentAssertUtils.ThrowIfNull(uris, nameof(uris));

            return this.SendBodyAsync<RemoveUrisRequest, PlaylistSnapshot>(
                HttpMethod.Delete,
                new RemoveUrisRequest { Tracks = uris.Select(item => new UriWithPositions { Uri = item }).ToArray() },
                cancellationToken);
        }

        public Task<PlaylistSnapshot> RemoveAsync(IEnumerable<UriWithPositions> urisWithPositions, string snapshotId, CancellationToken cancellationToken)
        {
            SpotifyArgumentAssertUtils.ThrowIfNull(urisWithPositions, nameof(urisWithPositions));

            return this.SendBodyAsync<RemoveUrisWithPositionsRequest, PlaylistSnapshot>(
                HttpMethod.Delete,
                new RemoveUrisWithPositionsRequest
                {
                    Tracks = urisWithPositions.Select(item => new UriWithPositions { Uri = item.Uri, Positions = item.Positions }).ToArray(),
                    SnapshotId = snapshotId
                },
                cancellationToken);
        }

        public Task<PlaylistSnapshot> RemoveAsync(IEnumerable<int> positions, string snapshotId, CancellationToken cancellationToken)
        {
            SpotifyArgumentAssertUtils.ThrowIfNull(positions, nameof(positions));

            return this.SendBodyAsync<RemovePositionsRequest, PlaylistSnapshot>(HttpMethod.Delete, new RemovePositionsRequest { Positions = positions.ToArray(), SnapshotId = snapshotId }, cancellationToken);
        }

        public Task<PlaylistSnapshot> ReorderAsync(int rangeStart, int insertBefore, int? rangeLength, string snapshotId, CancellationToken cancellationToken)
        {
            return this.SendBodyAsync<ReoderItemsRequest, PlaylistSnapshot>(
                HttpMethod.Put,
                new ReoderItemsRequest { RangeStart = rangeStart, InsertBefore = insertBefore, RangeLength = rangeLength, SnapshotId = snapshotId },
                cancellationToken);
        }

        public Task<PlaylistSnapshot> ReplaceAsync(IEnumerable<string> uris, CancellationToken cancellationToken)
        {
            SpotifyArgumentAssertUtils.ThrowIfNull(uris, nameof(uris));

            return this.SendBodyAsync<ReplaceUrisRequest, PlaylistSnapshot>(
                HttpMethod.Put,
                new ReplaceUrisRequest { Uris = uris.ToArray() },
                cancellationToken);
        }

        private class AddUrisRequest
        {
            [JsonPropertyName("uris")]
            public string[] Uris { get; set; }

            [JsonPropertyName("position")]
            public int? Position { get; set; }
        }

        private class RemoveUrisRequest
        {
            [JsonPropertyName("tracks")]
            public UriWithPositions[] Tracks { get; set; }
        }

        private class RemoveUrisWithPositionsRequest
        {
            [JsonPropertyName("tracks")]
            public UriWithPositions[] Tracks { get; set; }

            [JsonPropertyName("snapshot_id")]
            public string SnapshotId { get; set; }
        }

        private class RemovePositionsRequest
        {
            [JsonPropertyName("positions")]
            public int[] Positions { get; set; }

            [JsonPropertyName("snapshot_id")]
            public string SnapshotId { get; set; }
        }

        private class ReoderItemsRequest
        {
            [JsonPropertyName("range_start")]
            public int RangeStart { get; set; }

            [JsonPropertyName("insert_before")]
            public int InsertBefore { get; set; }

            [JsonPropertyName("range_length")]
            public int? RangeLength { get; set; }

            [JsonPropertyName("snapshot_id")]
            public string SnapshotId { get; set; }
        }

        private class ReplaceUrisRequest
        {
            [JsonPropertyName("uris")]
            public string[] Uris { get; set; }
        }
    }
}
