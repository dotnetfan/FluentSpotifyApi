using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Builder.Me.Following;
using FluentSpotifyApi.Builder.Me.Following.Playlist;
using FluentSpotifyApi.Builder.Me.Library;
using FluentSpotifyApi.Builder.Me.Personalization.RecentlyPlayed;
using FluentSpotifyApi.Builder.Me.Personalization.Top;
using FluentSpotifyApi.Builder.User;
using FluentSpotifyApi.Builder.User.Playlists;
using FluentSpotifyApi.Core.Model;
using FluentSpotifyApi.Model;

namespace FluentSpotifyApi.Builder.Me
{
    internal class Builder : BuilderBase, IMeBuilder, ILibraryBuilder, IPersonalizationBuilder, IFollowingBuilder
    {
        public Builder(ContextData contextData) : base(contextData, "me")
        {
        }

        ILibraryBuilder IMeBuilder.Library => this;

        IPersonalizationBuilder IMeBuilder.Personalization => this;

        IFollowingBuilder IMeBuilder.Following => this;

        ITopBuilder<FullArtist> IPersonalizationBuilder.TopArtists => Personalization.Top.Factory.CreateTopArtistsBuilder(this.ContextData, this.RouteValuesPrefix);

        ITopBuilder<FullTrack> IPersonalizationBuilder.TopTracks => Personalization.Top.Factory.CreateTopTracksBuilder(this.ContextData, this.RouteValuesPrefix);

        IRecentlyPlayedTracksBuilder IPersonalizationBuilder.RecentlyPlayedTracks => 
            new Personalization.RecentlyPlayed.RecentlyPlayedTracksBuilder(this.ContextData, this.RouteValuesPrefix);

        IPlaylistsBuilder IMeBuilder.Playlists => new UserBuilder(ContextData).Playlists;

        IGetLibraryEntitiesBuilder<SavedAlbum> ILibraryBuilder.Albums()
        {
            return Library.Factory.CreateGetLibraryAlbumsBuilder(this.ContextData, this.RouteValuesPrefix);
        }

        IManageLibraryEntitiesBuilder ILibraryBuilder.Albums(IEnumerable<string> ids)
        {
            return Library.Factory.CreateManageLibraryAlbumsBuilder(this.ContextData, this.RouteValuesPrefix, ids);
        }

        IGetLibraryEntitiesBuilder<SavedTrack> ILibraryBuilder.Tracks()
        {
            return Library.Factory.CreateGetLibraryTracksBuilder(this.ContextData, this.RouteValuesPrefix);
        }

        IManageLibraryEntitiesBuilder ILibraryBuilder.Tracks(IEnumerable<string> ids)
        {
            return Library.Factory.CreateManageLibraryTracksBuilder(this.ContextData, this.RouteValuesPrefix, ids);
        }

        IGetFollowedEntitiesBuilder<FollowedArtists> IFollowingBuilder.Artists()
        {
            return Following.Factory.CreateGetFollowedArtistsBuilder(this.ContextData, this.RouteValuesPrefix);
        }

        IManageFollowedEntitiesBuilder IFollowingBuilder.Artists(IEnumerable<string> ids)
        {
            return Following.Factory.CreateManageFollowedArtistsBuilder(this.ContextData, this.RouteValuesPrefix, ids);
        }

        IManageFollowedEntitiesBuilder IFollowingBuilder.Users(IEnumerable<string> ids)
        {
            return Following.Factory.CreateManageFollowedUsersBuilder(this.ContextData, this.RouteValuesPrefix, ids);
        }

        IFollowingPlaylistBuilder IFollowingBuilder.Playlist(string ownerId, string playlistId)
        {
            return new FollowingPlaylistBuilder(this.ContextData, ownerId, playlistId);
        }

        Task<PrivateUser> IMeBuilder.GetAsync(CancellationToken cancellationToken)
        {
            return this.GetAsync<PrivateUser>(cancellationToken);
        }

        public IPlaylistBuilder Playlist(string id)
        {
            return new UserBuilder(ContextData).Playlist(id);
        }
    }
}
