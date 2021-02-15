using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using FluentSpotifyApi.Core.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentSpotifyApi.Core.UnitTests.Utils
{
    [TestClass]
    public class SpotifySemaphoreUtilsTests
    {
        [TestMethod]
        public async Task ShouldExecuteAction()
        {
            // Arrange
            var semaphore = new SemaphoreSlim(1);

            // Act + Assert
            await SpotifySemaphoreUtils.ExecuteAsync(semaphore, async innerCt => await Task.Yield(), CancellationToken.None);
        }

        [TestMethod]
        public async Task ShouldExecuteFunc()
        {
            // Arrange
            var semaphore = new SemaphoreSlim(1);

            // Act
            var result = await SpotifySemaphoreUtils.ExecuteAsync(semaphore, async innerCt => { await Task.Yield(); return 3; }, CancellationToken.None);

            // Assert
            result.Should().Be(3);
        }
    }
}
