using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using FluentSpotifyApi.Core.Exceptions;
using FluentSpotifyApi.Core.HttpHandlers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;

namespace FluentSpotifyApi.Core.UnitTests.HttpHandlers
{
    [TestClass]
    public class SpotifyErrorHandlerTests
    {
        [TestMethod]
        public async Task ShouldReturnResponseWhenStatusCodeIsSuccess()
        {
            // Arrange
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.Expect("/test").Respond(HttpStatusCode.OK);

            var httpClient = this.CreateHttpClient(mockHttp);

            // Act
            await httpClient.GetAsync("http://localhost/test");

            // Assert
            mockHttp.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task ShouldThrowExceptionWhenStatusCodeIsNotSuccess()
        {
            // Arrange
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.Expect("/test").Respond(HttpStatusCode.BadGateway);

            var httpClient = this.CreateHttpClient(mockHttp);

            // Act + Assert
            (await ((Func<Task<HttpResponseMessage>>)(() => httpClient.GetAsync("http://localhost/test")))
                .Should()
                .ThrowExactlyAsync<SpotifyHttpResponseException>())
                .Where(ex => ex.ClientType == typeof(SpotifyErrorHandlerTests) && ex.ErrorCode == HttpStatusCode.BadGateway);

            mockHttp.VerifyNoOutstandingExpectation();
        }

        [TestMethod]
        public async Task ShouldThrowHttpResponseWithRegularErrorExceptionWhenStatusCodeIsNotSuccessAndPayloadIsRegularError()
        {
            // Arrange
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.Expect("/test").Respond(HttpStatusCode.BadRequest, "application/json", @"{""error"": { ""status"": 400, ""message"": ""bad request"" }}");

            var httpClient = this.CreateHttpClient(mockHttp);

            // Act + Assert
            (await ((Func<Task<HttpResponseMessage>>)(() => httpClient.GetAsync("http://localhost/test")))
                .Should()
                .ThrowExactlyAsync<SpotifyRegularErrorException>())
                .Where(ex => ex.ClientType == typeof(SpotifyErrorHandlerTests) &&
                    ex.ErrorCode == HttpStatusCode.BadRequest &&
                    ex.Error != null &&
                    ex.Error.Status == (int)HttpStatusCode.BadRequest &&
                    ex.Error.Message == "bad request");

            mockHttp.VerifyNoOutstandingExpectation();
        }

        private HttpClient CreateHttpClient(MockHttpMessageHandler mockHttp)
        {
            var errorResponseHandler = new SpotifyRegularErrorHandler<SpotifyErrorHandlerTests>()
            {
                InnerHandler = mockHttp
            };

            return new HttpClient(errorResponseHandler);
        }
    }
}
