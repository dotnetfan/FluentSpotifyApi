using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FluentSpotifyApi.Core.Model;
using FluentSpotifyApi.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentSpotifyApi.UnitTests
{
    [TestClass]
    [SuppressMessage("Microsoft.StyleCop.CSharp.SpacingRules", "SA1000:KeywordsMustBeSpacedCorrectly", Justification = "C# 7 Tuples")]
    public class ProfilesTests : TestBase
    {
        [TestMethod]
        public async Task ShouldGetCurrentUserProfileAsync()
        {
            // Arrange
            var mockResults = this.MockGet<PrivateUser>();

            // Act
            var result = await this.Client.Me.GetAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[0]);
            mockResults.First().RouteValues.Should().Equal(new[] { "me" });
            result.Should().BeSameAs(mockResults.First().Result);
        }

        [TestMethod]
        public async Task ShouldGetUserProfileAsync()
        {
            // Arrange
            const string userId = "User";

            var mockResults = this.MockGet<PublicUser>();

            // Act
            var result = await this.Client.User(userId).GetAsync();

            // Assert
            mockResults.Should().HaveCount(1);
            mockResults.First().QueryParameters.ShouldAllBeEquivalentTo(new(string Key, object Value)[0]);
            mockResults.First().RouteValues.Should().Equal(new[] { "users", userId });
            result.Should().BeSameAs(mockResults.First().Result);
        }
    }
}
