using System;
using System.Threading.Tasks;
using FluentAssertions;
using FluentSpotifyApi.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentSpotifyApi.IntegrationTests.CCF
{
    [TestClass]
    public class GetTests : TestBase
    {
        [TestMethod]
        [TestCategory(Settings.TestCategoryKey)]
        public async Task ShouldCallGetAsync()
        {
            // Arrange + Act
            var result = await this.Client.GetAsync<Page<PlaylistTrack>>(new Uri("https://api.spotify.com/v1/users/thelinmichael/playlists/7d2D2S200NyUE5KYs80PwO/tracks"));

            // Assert
            result.Items.Should().NotBeNull();
        }
    }
}
