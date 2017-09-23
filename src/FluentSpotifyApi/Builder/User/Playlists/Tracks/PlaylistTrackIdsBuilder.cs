using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model;

namespace FluentSpotifyApi.Builder.User.Playlists.Tracks
{
    internal class PlaylistTrackIdsBuilder : PlaylistTrackSequenceBuilderBase<string>, IPlaylistTrackIdsBuilder
    {
        public PlaylistTrackIdsBuilder(ContextData contextData, IEnumerable<object> routeValuesPrefix, string endpointName, IEnumerable<string> ids) 
            : base(contextData, routeValuesPrefix, endpointName, ids)
        {
        }

        public Task<PlaylistSnapshot> AddAsync(int? position, CancellationToken cancellationToken)
        {
            return this.SendAsync<PlaylistSnapshot, TrackUrls>(
                HttpMethod.Post,
                new TrackUrls { Uris = this.Sequence.Select(item => GetTrackUrl(item)).ToArray() },
                cancellationToken,
                optionalQueryStringParameters: new { position });
        }

        public Task<PlaylistSnapshot> RemoveAsync(CancellationToken cancellationToken)
        {
            return this.SendAsync<PlaylistSnapshot, TrackUrlWithPositions>(
                HttpMethod.Delete,
                new TrackUrlWithPositions { Tracks = this.Sequence.Select(item => new TrackUrlWithPosition { Uri = GetTrackUrl(item) }).ToArray() },
                cancellationToken);
        }

        public Task<PlaylistSnapshot> ReplaceAsync(CancellationToken cancellationToken)
        {
            return this.SendAsync<PlaylistSnapshot, TrackUrls>(
                HttpMethod.Put,
                new TrackUrls { Uris = this.Sequence.Select(item => GetTrackUrl(item)).ToArray() }, 
                cancellationToken);
        }
    }
}
