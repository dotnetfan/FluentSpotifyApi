using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Core.Model;
using FluentSpotifyApi.Core.Utils;

namespace FluentSpotifyApi.Builder.Users
{
    internal class UserBuilder : BuilderBase, IUserBuilder
    {
        public UserBuilder(RootBuilder root)
            : base(root, new object[] { "users", new CurrentUserIdPlaceholder() })
        {
        }

        public UserBuilder(RootBuilder root, string userId)
            : base(root, new[] { "users", userId })
        {
            SpotifyArgumentAssertUtils.ThrowIfNullOrEmpty(userId, nameof(userId));
        }

        public IUserPlaylistsBuilder Playlists => new UserPlaylistsBuilder(this);

        public Task<PublicUser> GetAsync(CancellationToken cancellationToken)
        {
            return this.GetAsync<PublicUser>(cancellationToken);
        }
    }
}
