using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using FluentSpotifyApi.Model;
using FluentSpotifyApi.Model.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentSpotifyApi.IntegrationTests.CCF
{
    [TestClass]
    public class PagingTests : TestBase
    {
        [TestMethod]
        [TestCategory(Settings.TestCategoryKey)]
        public async Task ShouldLoopThroughPagedResultAsync()
        {
            // Arrange + Act
            var pageResult = await this.Client.Search.Albums.Matching("artist:Metallica").GetAsync();
            var result = new List<SimpleAlbum>(pageResult.Page.Items);
            while (!string.IsNullOrEmpty(pageResult.Page.Next))
            {
                pageResult = await this.Client.GetAsync<SimpleAlbumsPageMessage>(new Uri(pageResult.Page.Next));
                result.AddRange(pageResult.Page.Items);
            }

            // Assert
            result.Should().NotBeEmpty();
        }
    }
}
