using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Expressions.Fields;
using FluentSpotifyApi.Extensions;
using FluentSpotifyApi.Model.Playlists;

namespace FluentSpotifyApi.Builder.Playlists
{
    internal class PlaylistBuilder : EntityBuilderBase, IPlaylistBuilder
    {
        public PlaylistBuilder(RootBuilder root, string id)
            : base(root, "playlists", id)
        {
        }

        public IPlaylistImagesBuilder Images => new PlaylistImagesBuilder(this);

        public IPlaylistItemsBuilder Items => new PlaylistItemsBuilder(this);

        public Task<Playlist> GetAsync(Action<IFieldsBuilder<Playlist>> buildFields, string market, CancellationToken cancellationToken)
            => this.GetAsync(FieldsProvider.Get(buildFields), market, cancellationToken);

        public Task<Playlist> GetAsync(string fields, string market, CancellationToken cancellationToken)
            => this.GetAsync<Playlist>(cancellationToken, queryParams: new { fields, market });

        public Task ChangeDetailsAsync(ChangePlaylistDetailsRequest changePlaylistDetailsRequest, CancellationToken cancellationToken)
            => this.SendBodyAsync(HttpMethod.Put, changePlaylistDetailsRequest, cancellationToken);

        public async Task<bool[]> CheckFollowersAsync(IEnumerable<string> userIds, CancellationToken cancellationToken)
            => await this.GetAsync<bool[]>(
                cancellationToken,
                additionalRouteValues: new[] { "followers", "contains" },
                queryParams: new { ids = userIds.JoinWithComma() })
            .ConfigureAwait(false);
    }
}
