using System;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using FluentSpotifyApi.Core.Exceptions;
using FluentSpotifyApi.Core.Internal.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentSpotifyApi.Core.UnitTests
{
    [TestClass]
    public class HttpResponseMessageExtensionsTests
    {
        [TestMethod]
        public async Task ShouldDoNothingWhenStatusCodeIsSuccessAsync()
        {
            // Arrange
            var responseMessage = new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.Accepted };

            // Act + Assert
            await responseMessage.EnsureSuccessStatusCodeAsync();
        }

        [TestMethod]
        public void ShouldThrowExceptionWhenStatusCodeIsNotSuccess()
        {
            // Arrange
            var responseMessage = new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.BadGateway, Content = new StringContent("exception test") };

            // Act + Assert
            ((Func<Task>)(() => responseMessage.EnsureSuccessStatusCodeAsync())).ShouldThrowExactly<SpotifyHttpResponseWithErrorCodeException>().WithMessage("exception test").Where(item => item.ErrorCode == System.Net.HttpStatusCode.BadGateway);
        }

        [TestMethod]
        public void ShouldThrowHttpResponseWithRegularErrorExceptionWhenStatusCodeIsNotSuccessAndPayloadIsRegularError()
        {
            // Arrange
            var responseMessage = new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.BadGateway, Content = new StringContent(@"{""error"": { ""status"": 502, ""message"": ""bad gateway"" }}") };

            // Act + Assert
            ((Func<Task>)(() => responseMessage.EnsureSuccessStatusCodeAsync())).ShouldThrow<SpotifyHttpResponseWithRegularErrorException>().WithMessage(@"{""error"": { ""status"": 502, ""message"": ""bad gateway"" }}").Where(item => item.ErrorCode == System.Net.HttpStatusCode.BadGateway && item.Payload.Status == 502 && item.Payload.Message == "bad gateway");
        }

        [TestMethod]
        public void ShouldThrowHttpResponseWithAuthenticationErrorExceptionWhenStatusCodeIsNotSuccessAndPayloadIsAuthenticationError()
        {
            // Arrange
            var responseMessage = new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.BadRequest, Content = new StringContent(@"{""error"": ""invalid_grant"", ""error_description"": ""invalid grant""}") };

            // Act + Assert
            ((Func<Task>)(() => responseMessage.EnsureSuccessStatusCodeAsync())).ShouldThrow<SpotifyHttpResponseWithAuthenticationErrorException>().WithMessage(@"{""error"": ""invalid_grant"", ""error_description"": ""invalid grant""}").Where(item => item.ErrorCode == System.Net.HttpStatusCode.BadRequest && item.Payload.Error == "invalid_grant" && item.Payload.ErrorDescription == "invalid grant");
        }
    }
}
