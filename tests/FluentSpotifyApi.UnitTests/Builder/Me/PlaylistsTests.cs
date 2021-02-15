using FluentSpotifyApi.Builder.Users;
using FluentSpotifyApi.UnitTests.Builder.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentSpotifyApi.UnitTests.Builder.Me
{
    [TestClass]
    public class PlaylistsTests : UserPlaylistsTestsBase
    {
        protected override IUserPlaylistsBuilder GetPlaylistsBuilder() => this.Client.Me.Playlists;
    }
}
