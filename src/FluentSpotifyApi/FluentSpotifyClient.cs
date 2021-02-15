using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Builder;
using FluentSpotifyApi.Builder.Albums;
using FluentSpotifyApi.Builder.Artists;
using FluentSpotifyApi.Builder.Browse;
using FluentSpotifyApi.Builder.Episodes;
using FluentSpotifyApi.Builder.Me;
using FluentSpotifyApi.Builder.Playlists;
using FluentSpotifyApi.Builder.Search;
using FluentSpotifyApi.Builder.Shows;
using FluentSpotifyApi.Builder.Tracks;
using FluentSpotifyApi.Builder.Users;
using FluentSpotifyApi.Core.User;
using FluentSpotifyApi.Core.Utils;

#pragma warning disable SA1201 // Elements should appear in the correct order

namespace FluentSpotifyApi
{
    internal class FluentSpotifyClient : IFluentSpotifyClient
    {
        private readonly ContextData contextData;
        private readonly BuilderBase.RootBuilder rootBuilder;

        public FluentSpotifyClient(IFluentSpotifyHttpClientFactory httpClientFactory, ICurrentUserProvider currentUserProvider)
        {
            this.contextData = new ContextData(httpClientFactory, currentUserProvider);
            this.rootBuilder = new BuilderBase.RootBuilder(this.contextData);
        }

        public IArtistBuilder Artists(string id) => new ArtistBuilder(this.rootBuilder, id);

        public IArtistsBuilder Artists(IEnumerable<string> ids) => new ArtistsBuilder(this.rootBuilder, ids);

        public IAlbumBuilder Albums(string id) => new AlbumBuilder(this.rootBuilder, id);

        public IAlbumsBuilder Albums(IEnumerable<string> ids) => new AlbumsBuilder(this.rootBuilder, ids);

        public ITrackBuilder Tracks(string id) => new TrackBuilder(this.rootBuilder, id);

        public ITracksBuilder Tracks(IEnumerable<string> ids) => new TracksBuilder(this.rootBuilder, ids);

        public IShowBuilder Shows(string id) => new ShowBuilder(this.rootBuilder, id);

        public IShowsBuilder Shows(IEnumerable<string> ids) => new ShowsBuilder(this.rootBuilder, ids);

        public IEpisodeBuilder Episodes(string id) => new EpisodeBuilder(this.rootBuilder, id);

        public IEpisodesBuilder Episodes(IEnumerable<string> ids) => new EpisodesBuilder(this.rootBuilder, ids);

        public IPlaylistBuilder Playlists(string id) => new PlaylistBuilder(this.rootBuilder, id);

        public IBrowseBuilder Browse => new BrowseBuilder(this.rootBuilder);

        public ISearchBuilder Search => new SearchBuilder(this.rootBuilder);

        public IMeBuilder Me => new MeBuilder(this.rootBuilder);

        public IUserBuilder Users(string id) => new UserBuilder(this.rootBuilder, id);

        public async Task<T> GetAsync<T>(Uri url, CancellationToken cancellationToken)
        {
            return await SpotifyHttpUtils.HandleTimeoutAsync<IFluentSpotifyClient, T>(
                async innerCt => await this.contextData.HttpClientFactory.CreateClient().GetFromJsonAsync<T>(url, innerCt).ConfigureAwait(false),
                cancellationToken).ConfigureAwait(false);
        }
    }
}
