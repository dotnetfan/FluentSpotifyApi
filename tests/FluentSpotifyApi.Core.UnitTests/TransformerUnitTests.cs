using FluentAssertions;
using FluentSpotifyApi.Core.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentSpotifyApi.Core.UnitTests
{
    [TestClass]
    public class TransformerUnitTests
    {
        [TestMethod]
        public void ShouldTransform()
        {
            // Arrange + Act + Assert
            new Transformer<int>(value => value + 1).Transform(3).Should().Be(4);
        }

        [TestMethod]
        public void ShouldHaveCorrectSourceType()
        {
            // Arrange + Act + Assert
            new Transformer<int>(value => value + 1).SourceType.Should().Be(typeof(int));
        }
    }
}
