using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FluentSpotifyApi.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentSpotifyApi.UnitTests
{
    [TestClass]
    [SuppressMessage("Microsoft.StyleCop.CSharp.SpacingRules", "SA1000:KeywordsMustBeSpacedCorrectly", Justification = "C# 7 Tuples")]
    public class UserTests : TestBase
    {
        [TestMethod]
        public async Task ShouldGetUserAsync()
        {
            // Arrange
            const string id = "wizzler";

            var mockResults = this.MockGet<PublicUser>();

            // Act
            var result = await this.Client.User(id).GetAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[0]);
            mockResults.First().RouteValues.Should().Equal(new[] { "users", id });
            result.Should().BeSameAs(mockResults.First().Result);
        }
    }
}
