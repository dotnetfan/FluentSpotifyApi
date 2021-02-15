using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;

namespace FluentSpotifyApi.UnitTests.Builder.Browse
{
    [TestClass]
    public class BrowseCategoriesTests : TestsBase
    {
        [TestMethod]
        public async Task ShouldGetCategories()
        {
            // Arrange
            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"browse/categories")
                .WithExactQueryString(string.Empty)
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            await this.Client.Browse.Categories().GetAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task ShouldGetCategoriesWithAllParams()
        {
            // Arrange
            const string country = "MX";
            const string locale = "es_MX";
            const int limit = 30;
            const int offset = 10;

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"browse/categories")
                .WithExactQueryString(new Dictionary<string, string>
                {
                    ["country"] = country,
                    ["locale"] = locale,
                    ["limit"] = limit.ToString(CultureInfo.InvariantCulture),
                    ["offset"] = offset.ToString(CultureInfo.InvariantCulture),
                })
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            await this.Client.Browse.Categories().GetAsync(country: country, locale: locale, limit: limit, offset: offset);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task ShouldGetCategory()
        {
            // Arrange
            const string categoryId = "party";

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"browse/categories/{categoryId}")
                .WithExactQueryString(string.Empty)
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            await this.Client.Browse.Categories(categoryId).GetAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task ShouldGetCategoryWithAllParams()
        {
            // Arrange
            const string categoryId = "party";
            const string country = "MX";
            const string locale = "es_MX";

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"browse/categories/{categoryId}")
                .WithExactQueryString(new Dictionary<string, string>
                {
                    ["country"] = country,
                    ["locale"] = locale,
                })
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            await this.Client.Browse.Categories(categoryId).GetAsync(country: country, locale: locale);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task ShouldGetCategoryPlaylists()
        {
            // Arrange
            const string categoryId = "party";

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"browse/categories/{categoryId}/playlists")
                .WithExactQueryString(string.Empty)
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            await this.Client.Browse.Categories(categoryId).Playlists.GetAsync();

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task ShouldGetCategoryPlaylistsWithAllParams()
        {
            // Arrange
            const string categoryId = "party";
            const string country = "MX";
            const int limit = 30;
            const int offset = 10;

            this.MockHttp
                .ExpectSpotifyRequest(HttpMethod.Get, $"browse/categories/{categoryId}/playlists")
                .WithExactQueryString(new Dictionary<string, string>
                {
                    ["country"] = country,
                    ["limit"] = limit.ToString(CultureInfo.InvariantCulture),
                    ["offset"] = offset.ToString(CultureInfo.InvariantCulture),
                })
                .WithNullContent()
                .Respond(HttpStatusCode.OK, "application/json", "{}");

            // Act
            var result = await this.Client.Browse.Categories(categoryId).Playlists.GetAsync(country: country, limit: limit, offset: offset);

            // Assert
            this.MockHttp.VerifyNoOutstandingExpectation();
            result.Should().NotBeNull();
        }
    }
}
