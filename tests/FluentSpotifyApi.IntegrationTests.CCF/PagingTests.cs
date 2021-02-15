using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using FluentSpotifyApi.Model.Albums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentSpotifyApi.IntegrationTests.CCF
{
    [TestClass]
    public class PagingTests : TestsBase
    {
        [TestMethod]
        [TestCategory(Settings.TestCategoryKey)]
        public async Task ShouldLoopThroughPagedResult()
        {
            // Arrange + Act
            var pageResult = await this.Client.Search.Albums.Matching(f => f.Artist == "Metallica").GetAsync();
            var result = new List<SimplifiedAlbum>(pageResult.Page.Items);
            while (!string.IsNullOrEmpty(pageResult.Page.Next))
            {
                pageResult = await this.Client.GetAsync<SimplifiedAlbumsPageResponse>(new Uri(pageResult.Page.Next));
                result.AddRange(pageResult.Page.Items);
            }

            // Assert
            result.Should().NotBeEmpty();
        }
    }
}
