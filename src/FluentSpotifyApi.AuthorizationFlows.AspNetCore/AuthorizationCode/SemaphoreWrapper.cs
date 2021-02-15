using System;
using System.Threading;

namespace FluentSpotifyApi.AuthorizationFlows.AspNetCore.AuthorizationCode
{
    internal class SemaphoreWrapper : IDisposable
    {
        private readonly SemaphoreSlim semaphore;

        public SemaphoreWrapper()
        {
            this.semaphore = new SemaphoreSlim(1);
        }

        public SemaphoreSlim Value => this.semaphore;

        public void Dispose()
        {
            this.semaphore.Dispose();
        }
    }
}
