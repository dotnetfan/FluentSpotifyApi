using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Builder.User.Playlists;
using FluentSpotifyApi.Core.Internal.Extensions;
using FluentSpotifyApi.Model;

namespace FluentSpotifyApi.Builder.User
{
    internal class UserBuilder : BuilderBase, IUserBuilder
    {
        public UserBuilder(ContextData contextData) : base(contextData, null, "users", UserTransformer.Yield())
        {
        }

        public UserBuilder(ContextData contextData, string userId) : base(contextData, null, "users", userId.Yield())
        {
        }

        public IPlaylistsBuilder Playlists => Factory.CreatePlaylistsBuilder(this.ContextData, this.RouteValuesPrefix);

        public IPlaylistBuilder Playlist(string id)
        {
            return Factory.CreatePlaylistBuilder(this.ContextData, this.RouteValuesPrefix, id);
        }

        public Task<PublicUser> GetAsync(CancellationToken cancellationToken)
        {
            return this.GetAsync<PublicUser>(cancellationToken);
        }
    }
}
