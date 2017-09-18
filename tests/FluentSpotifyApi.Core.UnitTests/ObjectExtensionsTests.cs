using System;
using System.Collections.Generic;
using FluentAssertions;
using FluentSpotifyApi.Core.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentSpotifyApi.Core.UnitTests
{
    [TestClass]
    public class ObjectExtensionsTests
    {
        [TestMethod]
        public void ShouldGetPropertyBag()
        {
            // Arrange
            var transformer = new Transformer<int>(null);
            var value = new
            {
                key1 = new Uri("http://localhost/test"),
                key2 = "test", parameters = new Dictionary<string, object> { { "key3", "test4" }, { "key4", 5.77f } },
                parameter = new KeyValuePair<string, object>("key5", 222),
                complex = new { key7 = true, key8 = false },
                transformer
            };

            // Act
            var result = SpotifyObjectHelpers.GetPropertyBag(value);

            // Assert
            result.ShouldBeEquivalentTo(new[]
            {
                new KeyValuePair<string, object>("key1", new Uri("http://localhost/test")),
                new KeyValuePair<string, object>("key2", "test"),
                new KeyValuePair<string, object>("key3", "test4"),
                new KeyValuePair<string, object>("key4", 5.77f),
                new KeyValuePair<string, object>("key5", 222),
                new KeyValuePair<string, object>("key7", true),
                new KeyValuePair<string, object>("key8", false),
                new KeyValuePair<string, object>("transformer", transformer),
            });
        }
    }
}
