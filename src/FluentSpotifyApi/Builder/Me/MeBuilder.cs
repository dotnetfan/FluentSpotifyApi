using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Builder.Me.Following;
using FluentSpotifyApi.Builder.Me.Library;
using FluentSpotifyApi.Builder.Me.Personalization;
using FluentSpotifyApi.Builder.Me.Player;
using FluentSpotifyApi.Builder.Users;
using FluentSpotifyApi.Core.Model;
using FluentSpotifyApi.Extensions;

namespace FluentSpotifyApi.Builder.Me
{
    internal class MeBuilder : BuilderBase, IMeBuilder
    {
        public MeBuilder(RootBuilder root)
            : base(root, "me".Yield())
        {
        }

        public IPersonalizationBuilder Personalization => new PersonalizationBuilder(this);

        public ILibraryBuilder Library => new LibraryBuilder(this);

        public IFollowingBuilder Following => new FollowingBuilder(this);

        public IUserPlaylistsBuilder Playlists => new UserBuilder(this.CreateRootBuilder()).Playlists;

        public IPlayerBuilder Player => new PlayerBuilder(this);

        public Task<PrivateUser> GetAsync(CancellationToken cancellationToken) => this.GetAsync<PrivateUser>(cancellationToken);
    }
}
