using FluentAssertions;
using FluentSpotifyApi.Core.Internal.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentSpotifyApi.Core.UnitTests
{
    [TestClass]
    public class ExtensionsTests
    {
        [TestMethod]
        public void ShouldYieldItem()
        {
            // Arrange + Act + Assert
            3.Yield().Should().Equal(new[] { 3 });
        }

        [TestMethod]
        public void ShouldCloneObjectUsingJsonSerializer()
        {
            // Arrange
            var test = new TestClass() { Id = "12" };

            // Act
            var clonedTest = test.CloneUsingJsonSerializer();

            // Assert
            clonedTest.Should().NotBeSameAs(test);
            clonedTest.ShouldBeEquivalentTo(test);
        } 

        private class TestClass
        {
            public string Id { get; set; }
        }
    }
}
