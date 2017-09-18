using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using FluentSpotifyApi.AuthorizationFlows.AuthorizationCode.Native;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;

namespace FluentSpotifyApi.AuthorizationFlows.UnitTests.AuthorizationCode.Native
{
    [TestClass]
    [SuppressMessage("Microsoft.StyleCop.CSharp.SpacingRules", "SA1009:ClosingParenthesisMustBeSpacedCorrectly", Justification = "C# 7 Tuples")]
    public class AuthenticationTicketStorageTests
    {
        private Mock<ISecureStorage> secureStorageMock;

        private IAuthenticationTicketStorage authenticationTicketStorage;

        [TestInitialize]
        public virtual void TestInitialize()
        {
            this.secureStorageMock = new Mock<ISecureStorage>(MockBehavior.Strict);
            this.authenticationTicketStorage = new AuthenticationTicketStorage(this.secureStorageMock.Object);
        }

        [TestMethod]
        public async Task GetShouldReturnNullWhenThereIsNoTicketStored()
        {
            // Arrange
            this.secureStorageMock.Setup(x => x.TryGetAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult<(bool, string)>((false, null))).Verifiable();

            // Act
            var result = await this.authenticationTicketStorage.GetAsync(CancellationToken.None);

            // Assert
            this.secureStorageMock.VerifyAll();
            result.Should().BeNull();
        }

        [TestMethod]
        public async Task GetShouldReturnStoredTicket()
        {
            // Arrange
            var item = new AuthenticationTicketStorageItem { AuthorizationKey = "testkey", Version = 1, User = new FluentSpotifyApi.Core.Model.PrivateUser { Id = "john" } };

            this.secureStorageMock.Setup(x => x.TryGetAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult<(bool, string)>((true, JsonConvert.SerializeObject(item)))).Verifiable();

            // Act
            var result = await this.authenticationTicketStorage.GetAsync(CancellationToken.None);

            // Assert
            this.secureStorageMock.VerifyAll();
            result.AuthorizationKey.Should().Be(item.AuthorizationKey);
            result.AccessToken.Should().BeNull();
            result.User.ShouldBeEquivalentTo(item.User);
        }

        [TestMethod]
        public async Task GetShouldReturnNullAndRemoveMalformedTicket()
        {
            // Arrange
            this.secureStorageMock.Setup(x => x.TryGetAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult<(bool, string)>((true, "malformed"))).Verifiable();
            this.secureStorageMock.Setup(x => x.RemoveAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(0)).Verifiable();

            // Act
            var result = await this.authenticationTicketStorage.GetAsync(CancellationToken.None);

            // Assert
            this.secureStorageMock.VerifyAll();
            result.Should().BeNull();           
        }

        [TestMethod]
        public async Task GetShouldReturnNullAndRemoveInvalidTicket()
        {
            // Arrange
            var item = new AuthenticationTicketStorageItem { AuthorizationKey = "testkey", Version = 1, User = new FluentSpotifyApi.Core.Model.PrivateUser() };

            this.secureStorageMock.Setup(x => x.TryGetAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult<(bool, string)>((true, JsonConvert.SerializeObject(item)))).Verifiable();
            this.secureStorageMock.Setup(x => x.RemoveAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(0)).Verifiable();

            // Act
            var result = await this.authenticationTicketStorage.GetAsync(CancellationToken.None);

            // Assert
            this.secureStorageMock.VerifyAll();
            result.Should().BeNull();
        }

        [TestMethod]
        public async Task GetShouldReturnNullAndRemoveTicketWithUnknownVersion()
        {
            // Arrange
            var item = new AuthenticationTicketStorageItem { AuthorizationKey = "testkey", Version = 2, User = new FluentSpotifyApi.Core.Model.PrivateUser() { Id = "john" } };

            this.secureStorageMock.Setup(x => x.TryGetAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult<(bool, string)>((true, JsonConvert.SerializeObject(item)))).Verifiable();
            this.secureStorageMock.Setup(x => x.RemoveAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(0)).Verifiable();

            // Act
            var result = await this.authenticationTicketStorage.GetAsync(CancellationToken.None);

            // Assert
            this.secureStorageMock.VerifyAll();
            result.Should().BeNull();
        }
    }
}
