using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentSpotifyApi.AuthorizationFlows.UnitTests.AuthorizationCode.Native
{
    [TestClass]
    public class AuthenticationManagerTests : TestBase
    {
        [TestMethod]
        public async Task ShouldExecuteAuthorizationCodeFlowWhenThereIsNoSessionStoredAsync()
        {
            // Arrange
            this.DateTimeOffsetProviderMock.Setup(x => x.GetUtcNow()).Returns(new System.DateTimeOffset(new System.DateTime(2015, 1, 1)));

            var authorizationKey = "test authorization key";

            var user = this.SetupAuthorizationFlow(authorizationKey: authorizationKey);

            // Act
            await this.AuthenticationManager.RestoreSessionOrAuthorizeUserAsync();

            // Assert
            this.AuthenticationManager.GetUser().ShouldBeEquivalentTo(user);
            this.SecureStorage.GetItem().AuthorizationKey.Should().Be(authorizationKey);
            this.SecureStorage.GetItem().User.ShouldBeEquivalentTo(user);
        }

        [TestMethod]
        public async Task ShouldRestoreSessionFromStorageAsync()
        {
            // Arrange
            var user = new FluentSpotifyApi.Core.Model.PrivateUser { Id = "testuser" };
            this.SecureStorage.SetItem(("testKey", user));

            // Act
            await this.AuthenticationManager.RestoreSessionOrAuthorizeUserAsync();

            // Assert
            this.AuthenticationManager.GetUser().ShouldBeEquivalentTo(user);
        }

        [TestMethod]
        public async Task ShouldCallRestoreSessionOrAuthorizeUserAsyncMultipleTimesWithoutErrorAsync()
        {
            // Arrange
            this.DateTimeOffsetProviderMock.Setup(x => x.GetUtcNow()).Returns(new System.DateTimeOffset(new System.DateTime(2015, 1, 1)));

            this.SetupAuthorizationFlow();

            // Act + Assert
            await this.AuthenticationManager.RestoreSessionOrAuthorizeUserAsync();
            await this.AuthenticationManager.RestoreSessionOrAuthorizeUserAsync();
        }

        [TestMethod]
        public void GetUserShouldReturnNullWhenRestoreSessionFromStorageOrAuthorizeUserWasNotCalled()
        {
            // Arrange + Act + Assert
            this.AuthenticationManager.GetUser().Should().BeNull();
        }

        [TestMethod]
        public async Task ShouldRemoveSessionFromStorageAsync()
        {
            // Arrange
            var user = new FluentSpotifyApi.Core.Model.PrivateUser { Id = "testuser" };
            this.SecureStorage.SetItem(("testKey", user));

            // Act
            await this.AuthenticationManager.RemoveSessionAsync();

            // Assert
            this.SecureStorage.GetItem().Should().Be((null, null));
        }

        [TestMethod]
        public async Task ShouldCallRemoveSessionAsyncMultipleTimesWithoutErrorAsync()
        {
            // Arrange
            var user = new FluentSpotifyApi.Core.Model.PrivateUser { Id = "testuser" };
            this.SecureStorage.SetItem(("testKey", user));

            // Act + Assert
            await this.AuthenticationManager.RemoveSessionAsync();
            await this.AuthenticationManager.RemoveSessionAsync();
        }
    }
}
