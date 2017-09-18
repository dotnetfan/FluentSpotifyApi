using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Builder;
using FluentSpotifyApi.Builder.Albums;
using FluentSpotifyApi.Builder.Artists;
using FluentSpotifyApi.Builder.Browse;
using FluentSpotifyApi.Builder.Me;
using FluentSpotifyApi.Builder.Search;
using FluentSpotifyApi.Builder.Tracks;
using FluentSpotifyApi.Builder.User;
using FluentSpotifyApi.Client;
using FluentSpotifyApi.Core.Options;
using FluentSpotifyApi.Options;

namespace FluentSpotifyApi
{
    internal class FluentSpotifyClient : IFluentSpotifyClient
    {
        private readonly ISpotifyHttpClient spotifyHttpClient;

        private readonly ContextData contextData;

        public FluentSpotifyClient(ISpotifyHttpClient spotifyHttpClient, IOptionsProvider<FluentSpotifyClientOptions> fluentSpotifyClientOptionsProvider)
        {
            this.contextData = new ContextData(spotifyHttpClient, fluentSpotifyClientOptionsProvider);
            this.spotifyHttpClient = spotifyHttpClient;
        }

        public IBrowseBuilder Browse => new BrowseBuilder(this.contextData);

        public IMeBuilder Me => new Builder.Me.Builder(this.contextData);

        public ISearchBuilder Search => new SearchBuilder(this.contextData);

        public IAlbumBuilder Album(string id) => Builder.Albums.Factory.CreateAlbumBuilder(this.contextData, id);

        public IAlbumsBuilder Albums(IEnumerable<string> ids) => Builder.Albums.Factory.CreateAlbumsBuilder(this.contextData, ids);

        public IArtistBuilder Artist(string id) => Builder.Artists.Factory.CreateArtistBuilder(this.contextData, id);

        public IArtistsBuilder Artists(IEnumerable<string> ids) => Builder.Artists.Factory.CreateArtistsBuilder(this.contextData, ids);

        public ITrackBuilder Track(string id) => Builder.Tracks.Factory.CreateTrackBuilder(this.contextData, id);

        public ITracksBuilder Tracks(IEnumerable<string> ids) => Builder.Tracks.Factory.CreateTracksBuilder(this.contextData, ids);

        public IUserBuilder User(string id) => new Builder.User.UserBuilder(this.contextData, id);

        public Task<T> GetAsync<T>(Uri url, CancellationToken cancellationToken) => this.contextData.SpotifyHttpClient.SendAsync<T>(url, HttpMethod.Get, null, null, null, cancellationToken);
    }
}
