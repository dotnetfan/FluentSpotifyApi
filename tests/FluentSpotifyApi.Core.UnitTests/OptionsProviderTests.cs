using System;
using FluentAssertions;
using FluentSpotifyApi.Core.Options;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FluentSpotifyApi.Core.UnitTests
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

        public void ShouldThrowValidationExceptionWhenOptionsAreInvalid()
        {
            // Arrange
            var testOptions = new TestOptions();

            var optionsMock = new Mock<IOptions<TestOptions>>();
            optionsMock.Setup(x => x.Value).Returns(testOptions);

            var optionsProvider = new OptionsProvider<TestOptions>(optionsMock.Object);

            // Act + Assert
            ((Action)(() => optionsProvider.Get())).ShouldThrow<ArgumentNullException>().Which.ParamName.Should().Be("Option");
        }

        public class TestOptions : Options.IValidatable
        {
            public string Option { get; set; }

            public void Validate()
            {
                if (string.IsNullOrEmpty(this.Option))
                {
                    throw new ArgumentNullException(nameof(this.Option));
                }
            }
        }
    }
}
