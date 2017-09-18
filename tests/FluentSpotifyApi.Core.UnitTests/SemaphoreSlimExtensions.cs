using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using FluentSpotifyApi.Core.Internal.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentSpotifyApi.Core.UnitTests
{
    [TestClass]
    public class SemaphoreSlimExtensions
    {
        public async Task ShouldExecuteActionAsync()
        {
            // Arrange
            var semaphore = new SemaphoreSlim(1);

            // Act + Assert
            await semaphore.ExecuteAsync(async innerCt => await Task.Yield(), CancellationToken.None);
        }

        public async Task ShouldExecuteFuncAsync()
        {
            // Arrange
            var semaphore = new SemaphoreSlim(1);

            // Act
            var result = await semaphore.ExecuteAsync(async innerCt => { await Task.Yield(); return 3; }, CancellationToken.None);

            // Assert
            result.Should().Be(3);
        }
    }
}
