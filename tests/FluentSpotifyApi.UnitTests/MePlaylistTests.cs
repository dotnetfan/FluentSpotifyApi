using System.Threading.Tasks;
using FluentSpotifyApi.Builder.User.Playlists;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentSpotifyApi.UnitTests
{
    [TestClass]
    public class MePlaylistTests : PlaylistTestsBase
    {
        [TestMethod]
        public async Task ShouldGetMyPlaylistsAsync()
        {
            await this.ShouldGetPlaylistsAsync();
        }

        [TestMethod]
        public async Task ShouldGetMyPlaylistsWithDefaultsAsync()
        {
            await this.ShouldGetPlaylistsWithDefaultsAsync();
        }

        [TestMethod]
        public async Task ShouldGetMyPlaylistAsync()
        {
            await this.ShouldGetPlaylistsAsync();
        }

        [TestMethod]
        public async Task ShouldGetMyPlaylistTracksAsync()
        {
            await this.ShouldGetPlaylistTracksAsync();
        }

        [TestMethod]
        public async Task ShouldGetMyPlaylistTracksWithDefaultsAsync()
        {
            await this.ShouldGetPlaylistTracksWithDefaultsAsync();
        }

        [TestMethod]
        public async Task ShouldCreateMyPlaylistAsync()
        {
            await this.ShouldCreatePlaylistAsync();
        }

        [TestMethod]
        public async Task ShouldCreateMyPlaylistWithDefaultsAsync()
        {
            await this.ShouldCreatePlaylistWithDefaultsAsync();
        }

        [TestMethod]
        public async Task ShouldAddTrackToMyPlaylistAtPositionAsync()
        {
            await this.ShouldAddTrackToPlaylistAtPositionAsync();
        }

        [TestMethod]
        public async Task ShouldAddTrackToMyPlaylistAsync()
        {
            await this.ShouldAddTrackToPlaylistAsync();
        }

        [TestMethod]
        public async Task ShouldRemoveAllOccurrencesOfTracksFromMyPlaylistAsync()
        {
            await this.ShouldRemoveAllOccurrencesOfTracksFromPlaylistAsync();
        }

        [TestMethod]
        public async Task ShouldRemoveSpecificOccurrencesOfTracksFromMyPlaylistAsync()
        {
            await this.ShouldRemoveSpecificOccurrencesOfTracksFromPlaylistAsync();
        }

        [TestMethod]
        public async Task ShouldRemoveTracksAtGivenPositionsFromMyPlaylistAsync()
        {
            await this.ShouldRemoveTracksAtGivenPositionsFromPlaylistAsync();
        }

        [TestMethod]
        public async Task ShouldReorderMyPlaylistTracksAsync()
        {
            await this.ShouldReorderPlaylistTracksAsync();
        }

        [TestMethod]
        public async Task ShouldReorderMyPlaylistTracksWithDefaultsAsync()
        {
            await this.ShouldReorderPlaylistTracksWithDefaultsAsync();
        }

        [TestMethod]
        public async Task ShouldReplaceMyPlaylistTracksAsync()
        {
            await this.ShouldReplacePlaylistTracksAsync();
        }

        [TestMethod]
        public async Task ShouldUpdateMyPlaylistAsync()
        {
            await this.ShouldUpdatePlaylistAsync();
        }

        [TestMethod]
        public async Task ShouldUpdateMyPlaylistCoverAsync()
        {
            await this.ShouldUpdatePlaylistCoverAsync();
        }

        protected override IPlaylistBuilder GetPlaylistBuilder(string id)
        {
            return this.Client.Me.Playlist(id);
        }

        protected override IPlaylistsBuilder GetPlaylistsBuilder()
        {
            return this.Client.Me.Playlists;
        }        
    }
}
