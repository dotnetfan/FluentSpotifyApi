using FluentSpotifyApi.Builder.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentSpotifyApi.UnitTests.Builder.Users
{
    [TestClass]
    public class PlaylistsTests : UserPlaylistsTestsBase
    {
        protected override IUserPlaylistsBuilder GetPlaylistsBuilder() => this.Client.Users(TestsBase.UserId).Playlists;
    }
}
