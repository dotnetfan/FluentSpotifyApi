using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace FluentSpotifyApi.AuthorizationFlows.AspNetCore.AuthorizationCode
{
    internal class SemaphoreProvider : ISemaphoreProvider
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public SemaphoreProvider(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public SemaphoreSlim Get()
        {
            return this.httpContextAccessor.HttpContext.RequestServices.GetRequiredService<SemaphoreWrapper>().Value;
        }
    }
}
