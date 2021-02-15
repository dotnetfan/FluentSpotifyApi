using FluentAssertions;
using FluentSpotifyApi.Core.Options;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FluentSpotifyApi.Core.UnitTests.Options
{
    [TestClass]
    public class OptionsProviderTests
    {
        [TestMethod]
        public void ShouldGetOptions()
        {
            // Arrange
            var testOptions = new TestOptions() { Option = "option" };

            var optionsMock = new Mock<IOptions<TestOptions>>();
            optionsMock.Setup(x => x.Value).Returns(testOptions);

            var optionsValue = new OptionsProvider<TestOptions>(optionsMock.Object);

            // Act + Assert
            optionsValue.Get().Should().BeSameAs(testOptions);
        }

        public class TestOptions
        {
            public string Option { get; set; }
        }
    }
}
