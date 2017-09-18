using System;
using System.Threading;
using System.Threading.Tasks;

namespace FluentSpotifyApi.Core.Internal.Extensions
{
    /// <summary>
    /// The set of <see cref="SemaphoreSlim"/> extensions.
    /// </summary>
    public static class SemaphoreSlimExtensions
    {
        /// <summary>
        /// Executes asynchronous action inside semaphore.
        /// </summary>
        /// <param name="semaphore">The semaphore.</param>
        /// <param name="func">The function.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public static Task ExecuteAsync(this SemaphoreSlim semaphore, Func<CancellationToken, Task> func, CancellationToken cancellationToken = default(CancellationToken))
        {
            return semaphore.ExecuteAsync(async (innerCt) => { await func(innerCt).ConfigureAwait(false); return 0; }, cancellationToken);
        }

        /// <summary>
        /// Executes asynchronous function inside semaphore.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="semaphore">The semaphore.</param>
        /// <param name="func">The function.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public static async Task<T> ExecuteAsync<T>(this SemaphoreSlim semaphore, Func<CancellationToken, Task<T>> func, CancellationToken cancellationToken = default(CancellationToken))
        {
            await semaphore.WaitAsync(cancellationToken).ConfigureAwait(false);
            try
            {
                return await func(cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                semaphore.Release();
            }
        }
    }
}
