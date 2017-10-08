using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Builder.User.Playlists.Tracks;
using FluentSpotifyApi.Expressions.Fields;
using FluentSpotifyApi.Model;

namespace FluentSpotifyApi.Builder.User.Playlists
{
    internal class PlaylistBuilder : EntityBuilderBase, IPlaylistBuilder
    {
        public PlaylistBuilder(ContextData contextData, IEnumerable<object> routeValuesPrefix, string endpointName, string id) : base(contextData, routeValuesPrefix, endpointName, id)
        {
        }

        public Task<FullPlaylist> GetAsync(Action<IFieldsBuilder<FullPlaylist>> buildFields, string market, CancellationToken cancellationToken)
        {
            return this.GetAsync(FieldsProvider.Get(buildFields), market, cancellationToken);
        }

        public Task<FullPlaylist> GetAsync(string fields, string market, CancellationToken cancellationToken)
        {
            return this.GetAsync<FullPlaylist>(cancellationToken, optionalQueryStringParameters: new { fields, market });
        }

        public Task UpdateAsync(UpdatePlaylistDto updatePlaylistDto, CancellationToken cancellationToken)
        {
            return this.SendAsync<object, UpdatePlaylistDto>(HttpMethod.Put, updatePlaylistDto, cancellationToken);
        }

        public Task UpdateCoverAsync(Func<CancellationToken, Task<Stream>> coverStreamProvider, CancellationToken cancellationToken)
        {
            return this.SendAsync<object>(HttpMethod.Put, coverStreamProvider, "image/jpeg", cancellationToken, additionalRouteValues: new object[] { "images" });
        }

        public IPlaylistTracksBuilder Tracks()
        {
            return Playlists.Tracks.Factory.CreatePlaylistTracksBuilder(this.ContextData, this.RouteValuesPrefix);
        }

        public IPlaylistTrackIdsBuilder Tracks(IEnumerable<string> ids)
        {
            return Playlists.Tracks.Factory.CreatePlaylistTrackIdsBuilder(this.ContextData, this.RouteValuesPrefix, ids);
        }

        public IPlaylistTrackPositionsBuilder Tracks(IEnumerable<int> positions)
        {
            return Playlists.Tracks.Factory.CreatePlaylistTrackPositionsBuilder(this.ContextData, this.RouteValuesPrefix, positions);
        }

        [SuppressMessage("Microsoft.StyleCop.CSharp.SpacingRules", "SA1009:ClosingParenthesisMustBeSpacedCorrectly", Justification = "C# 7 Tuples")]
        public IPlaylistTrackIdsWithPositionsBuilder Tracks(IEnumerable<(string Id, int[] Positions)> idsWithPositions)
        {
            return Playlists.Tracks.Factory.CreatePlaylistTrackIdsWithPositionsBuilder(this.ContextData, this.RouteValuesPrefix, idsWithPositions);
        }

        public async Task<IList<bool>> CheckFollowersAsync(IEnumerable<string> userIds, CancellationToken cancellationToken)
        {
            return await new CheckFollowersBuilder(this.ContextData, this.RouteValuesPrefix, userIds).CheckAsync(cancellationToken).ConfigureAwait(false);
        }

        private class CheckFollowersBuilder : EntitiesBuilderBase
        {
            public CheckFollowersBuilder(ContextData contextData, IEnumerable<object> routeValuesPrefix, IEnumerable<string> ids) 
                : base(contextData, routeValuesPrefix, "followers", ids)
            {
            }

            public Task<bool[]> CheckAsync(CancellationToken cancellationToken)
            {
                return this.GetListAsync<bool[]>(cancellationToken, additionalRouteValues: new[] { "contains" });
            }
        }
    }
}
