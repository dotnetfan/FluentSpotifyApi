using System;
using System.Threading.Tasks;
using FluentAssertions;
using FluentSpotifyApi.AuthorizationFlows.AuthorizationCode.Exceptions;
using FluentSpotifyApi.AuthorizationFlows.AuthorizationCode.Native;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentSpotifyApi.AuthorizationFlows.UnitTests.AuthorizationCode.Native
{
    [TestClass]
    public class AuthenticationManagerTests : TestBase
    {
        [TestMethod]
        public async Task ShouldExecuteAuthorizationCodeFlowWhenThereIsNoSessionStoredWhenRestoreSessionOrAuthorizeUserAsyncIsCalledAsync()
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
        public async Task ShouldRestoreSessionFromStorageWhenRestoreSessionOrAuthorizeUserAsyncIsCalledAsync()
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
        public async Task ShouldRestoreSessionFromStorageWhenRestoreSessionAsyncIsCalledAsync()
        {
            // Arrange
            var user = new FluentSpotifyApi.Core.Model.PrivateUser { Id = "testuser" };
            this.SecureStorage.SetItem(("testKey", user));

            // Act
            await this.AuthenticationManager.RestoreSessionAsync();

            // Assert
            this.AuthenticationManager.GetUser().ShouldBeEquivalentTo(user);
        }

        [TestMethod]
        public void RestoreSessionAsyncShouldThrowSessionNotFoundExceptionWhenThereIsNoSessionInStorage()
        {
            // Arrange + Act + Assert
            ((Func<Task>)(() => this.AuthenticationManager.RestoreSessionAsync())).ShouldThrow<SessionNotFoundException>();
        }

        [TestMethod]
        public async Task ShouldCallRestoreSessionAsyncMultipleTimesWithoutErrorAsync()
        {
            // Arrange
            var user = new FluentSpotifyApi.Core.Model.PrivateUser { Id = "testuser" };
            this.SecureStorage.SetItem(("testKey", user));

            // Act + Assert
            await this.AuthenticationManager.RestoreSessionAsync();
            await this.AuthenticationManager.RestoreSessionAsync();
        }

        [TestMethod]
        public async Task GetSessionStateAsyncShouldReturnNotFoundWhenThereIsNoSessionStoredAsync()
        {
            // Arrange + Act + Assert
            (await this.AuthenticationManager.GetSessionStateAsync()).Should().Be(SessionState.NotFound);
        }

        [TestMethod]
        public async Task GetSessionStateAsyncShouldReturnStoredInLocalStorageWhenThereIsASessionInStorageThatHasNotBeenLoadedYetAsync()
        {
            // Arrange
            var user = new FluentSpotifyApi.Core.Model.PrivateUser { Id = "testuser" };
            this.SecureStorage.SetItem(("testKey", user));

            // Act + Assert
            (await this.AuthenticationManager.GetSessionStateAsync()).Should().Be(SessionState.StoredInLocalStorage);
        }

        [TestMethod]
        public async Task GetSessionStateAsyncShouldReturnCachedInMemoryWhenThereIsASessionInMemoryAsync()
        {
            // Arrange
            var user = new FluentSpotifyApi.Core.Model.PrivateUser { Id = "testuser" };
            this.SecureStorage.SetItem(("testKey", user));

            // Act
            await this.AuthenticationManager.RestoreSessionAsync();
            var result = await this.AuthenticationManager.GetSessionStateAsync();

            // Assert
            result.Should().Be(SessionState.CachedInMemory);
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
