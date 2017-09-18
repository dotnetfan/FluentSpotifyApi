using System.Threading.Tasks;
using FluentSpotifyApi.Builder.User.Playlists;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentSpotifyApi.UnitTests
{
    [TestClass]
    public class UserPlaylistTests : PlaylistTestsBase
    {
        [TestMethod]
        public async Task ShouldGetUserPlaylistsAsync()
        {
            await this.ShouldGetPlaylistsAsync();
        }

        [TestMethod]
        public async Task ShouldGetUserPlaylistsWithDefaultsAsync()
        {
            await this.ShouldGetPlaylistsWithDefaultsAsync();
        }

        [TestMethod]
        public async Task ShouldGetUserPlaylistAsync()
        {
            await this.ShouldGetPlaylistsAsync();
        }

        [TestMethod]
        public async Task ShouldGetUserPlaylistTracksAsync()
        {
            await this.ShouldGetPlaylistTracksAsync();
        }

        [TestMethod]
        public async Task ShouldGetUserPlaylistTracksWithDefaultsAsync()
        {
            await this.ShouldGetPlaylistTracksWithDefaultsAsync();
        }

        [TestMethod]
        public async Task ShouldCreateUserPlaylistAsync()
        {
            await this.ShouldCreatePlaylistAsync();
        }

        [TestMethod]
        public async Task ShouldCreateUserPlaylistWithDefaultsAsync()
        {
            await this.ShouldCreatePlaylistWithDefaultsAsync();
        }

        [TestMethod]
        public async Task ShouldAddTrackToUserPlaylistAtPositionAsync()
        {
            await this.ShouldAddTrackToPlaylistAtPositionAsync();
        }

        [TestMethod]
        public async Task ShouldAddTrackToUserPlaylistAsync()
        {
            await this.ShouldAddTrackToPlaylistAsync();
        }

        [TestMethod]
        public async Task ShouldRemoveAllOccurrencesOfTracksFromUserPlaylistAsync()
        {
            await this.ShouldRemoveAllOccurrencesOfTracksFromPlaylistAsync();
        }

        [TestMethod]
        public async Task ShouldRemoveSpecificOccurrencesOfTracksFromUserPlaylistAsync()
        {
            await this.ShouldRemoveSpecificOccurrencesOfTracksFromPlaylistAsync();
        }

        [TestMethod]
        public async Task ShouldRemoveTracksAtGivenPositionsFromUserPlaylistAsync()
        {
            await this.ShouldRemoveTracksAtGivenPositionsFromPlaylistAsync();
        }

        [TestMethod]
        public async Task ShouldReorderUserPlaylistTracksAsync()
        {
            await this.ShouldReorderPlaylistTracksAsync();
        }

        [TestMethod]
        public async Task ShouldReorderUserPlaylistTracksWithDefaultsAsync()
        {
            await this.ShouldReorderPlaylistTracksWithDefaultsAsync();
        }

        [TestMethod]
        public async Task ShouldReplaceUserPlaylistTracksAsync()
        {
            await this.ShouldReplacePlaylistTracksAsync();
        }

        [TestMethod]
        public async Task ShouldUpdateUserPlaylistAsync()
        {
            await this.ShouldUpdatePlaylistAsync();
        }

        protected override IPlaylistBuilder GetPlaylistBuilder(string id)
        {
            return this.Client.User(TestBase.UserId).Playlist(id);
        }

        protected override IPlaylistsBuilder GetPlaylistsBuilder()
        {
            return this.Client.User(TestBase.UserId).Playlists;
        }
    }
}
