using FluentAssertions;
using FluentSpotifyApi.Core.Internal.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentSpotifyApi.Core.UnitTests
{
    [TestClass]
    public class EnumExtensionsTests
    {
        private enum TestEnum
        {
            [System.ComponentModel.Description("Test1")]
            TestA,

            [System.ComponentModel.Description("Test2")]
            TestB,
        }

        [TestMethod]
        public void ShouldGetDescription()
        {
            // Arrange + Act + Assert
            TestEnum.TestA.GetDescription().Should().Be("Test1");
        }

        [TestMethod]
        public void ShouldGetAllValues()
        {
            // Arrange + Act + Assert
            EnumExtensions.GetValues<TestEnum>().Should().Equals(new[] { TestEnum.TestA, TestEnum.TestB });
        }
    }
}
