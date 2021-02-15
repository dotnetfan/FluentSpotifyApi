using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FluentSpotifyApi.Model.Tracks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentSpotifyApi.IntegrationTests.CCF
{
    [TestClass]
    public class PlaylistsTests : TestsBase
    {
        [TestMethod]
        [TestCategory(Settings.TestCategoryKey)]
        public async Task ShouldGetUsersPlaylist()
        {
            // Arrange + Act
            var playlists = await this.Client.Users("metalsucks").Playlists.GetAsync();

            // Assert
            playlists.Items.Should().NotBeEmpty();
        }

        [TestMethod]
        [TestCategory(Settings.TestCategoryKey)]
        public async Task ShouldGetPlaylist()
        {
            // Arrange
            const string id = "40C5k2GWBlficlUyQKmR0S";

            // Act
            var playlist = await this.Client.Playlists(id).GetAsync();

            // Assert
            playlist.Should().NotBeNull();
            playlist.Id.Should().Be(id);
            playlist.Tracks.Items.Should().NotBeEmpty();
        }

        [TestMethod]
        [TestCategory(Settings.TestCategoryKey)]
        public async Task ShouldGetPlaylistWithOnlySomeProperties()
        {
            // Arrange
            const string id = "40C5k2GWBlficlUyQKmR0S";

            // Act
            var playlist = await this.Client.Playlists(id).GetAsync(b => b
                .Include(p => p.Id)
                .Include(p => p.Tracks.Items[0].Track.Type)
                .Include(p => p.Tracks.Items[0].Track.Id));

            // Assert
            playlist.Should().NotBeNull();
            playlist.Id.Should().Be(id);
            playlist.Name.Should().BeNull();
            playlist.Tracks.Items.Should().NotBeEmpty();
            playlist.Tracks.Items.Should().OnlyContain(x => x.Track.Type != null && x.Track.Id != null && x.Track.Href == null);
        }

        [TestMethod]
        [TestCategory(Settings.TestCategoryKey)]
        public async Task ShouldGetPlaylistImages()
        {
            // Arrange
            const string id = "40C5k2GWBlficlUyQKmR0S";

            // Act
            var images = await this.Client.Playlists(id).Images.GetAsync();

            // Assert
            images.Should().NotBeEmpty();
        }

        [TestMethod]
        [TestCategory(Settings.TestCategoryKey)]
        public async Task ShouldGetPlaylistItems()
        {
            // Arrange
            const string id = "40C5k2GWBlficlUyQKmR0S";

            // Act
            var page = await this.Client.Playlists(id).Items.GetAsync();

            // Assert
            page.Should().NotBeNull();
            page.Items.Should().NotBeEmpty();
        }

        [TestMethod]
        [TestCategory(Settings.TestCategoryKey)]
        public async Task ShouldGetPlaylistTracksWithoutAlbumsAndArtists()
        {
            // Arrange
            const string id = "40C5k2GWBlficlUyQKmR0S";

            // Act
            var page = await this.Client.Playlists(id).Items.GetAsync(b => b
                .Exclude(t => ((Track)t.Items[0].Track).Album)
                .Exclude(t => ((Track)t.Items[0].Track).Artists));

            // Assert
            page.Should().NotBeNull();
            page.Items.Select(x => x.Track).OfType<Track>().Should().NotBeEmpty();
            page.Items.Select(x => x.Track).OfType<Track>().Should().OnlyContain(x => x.Id != null && x.Album == null && x.Artists == null);
        }

        [TestMethod]
        [TestCategory(Settings.TestCategoryKey)]
        public async Task ShouldCheckIfUserFollowsPlaylist()
        {
            // Arrange
            const string id = "40C5k2GWBlficlUyQKmR0S";

            // Act
            var result = await this.Client.Playlists(id).CheckFollowersAsync(new[] { "wizzler" });

            // Assert
            result.Should().HaveCount(1);
        }
    }
}
