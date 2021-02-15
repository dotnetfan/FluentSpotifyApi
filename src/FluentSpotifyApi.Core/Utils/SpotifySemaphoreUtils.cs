using System;
using System.Threading;
using System.Threading.Tasks;

namespace FluentSpotifyApi.Core.Utils
{
    /// <summary>
    /// The set of <see cref="SemaphoreSlim"/> utilities intended for FluentSpotifyApi library usage.
    /// </summary>
    public static class SpotifySemaphoreUtils
    {
        /// <summary>
        /// Executes asynchronous action inside semaphore.
        /// </summary>
        /// <param name="semaphore">The semaphore.</param>
        /// <param name="action">The action.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public static Task ExecuteAsync(SemaphoreSlim semaphore, Func<CancellationToken, Task> action, CancellationToken cancellationToken)
        {
            return ExecuteAsync(semaphore, async (innerCt) => { await action(innerCt).ConfigureAwait(false); return 0; }, cancellationToken);
        }

        /// <summary>
        /// Executes asynchronous function inside semaphore.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="semaphore">The semaphore.</param>
        /// <param name="func">The function.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public static async Task<T> ExecuteAsync<T>(SemaphoreSlim semaphore, Func<CancellationToken, Task<T>> func, CancellationToken cancellationToken)
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
