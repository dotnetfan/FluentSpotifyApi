using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using FluentSpotifyApi.Core.Exceptions;
using FluentSpotifyApi.Model.Albums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentSpotifyApi.IntegrationTests.CCF
{
    [TestClass]
    public class GetTests : TestsBase
    {
        [TestMethod]
        [TestCategory(Settings.TestCategoryKey)]
        public async Task ShouldCallGet()
        {
            // Arrange + Act
            var result = await this.Client.GetAsync<Album>(new Uri("https://api.spotify.com/v1/albums/6akEvsycLGftJxYudPjmqK"));

            // Assert
            result.Id.Should().Be("6akEvsycLGftJxYudPjmqK");
        }

        [TestMethod]
        [TestCategory(Settings.TestCategoryKey)]
        public async Task ShouldThrowException()
        {
            // Arrange + Act + Assert
            (await ((Func<Task<Album>>)(() => this.Client.GetAsync<Album>(new Uri("https://api.spotify.com/v1/albums/xyz"))))
                .Should()
                .ThrowAsync<SpotifyRegularErrorException>())
                .Where(ex => ex.ClientType == typeof(IFluentSpotifyClient) && ex.Error != null && ex.Error.Status == (int)HttpStatusCode.BadRequest);
        }
    }
}
