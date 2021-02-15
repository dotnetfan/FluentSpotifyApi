using System;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Core;

namespace FluentSpotifyApi.AuthorizationFlows.UWP.Extensions
{
    internal static class DispatcherExtensions
    {
        public static Task RunDirectlyOrDispatchAsync(this CoreDispatcher dispatcher, Action action, CancellationToken cancellationToken)
        {
            return dispatcher.RunDirectlyOrDispatchAsync(() => { action(); return 0; }, cancellationToken);
        }

        public static async Task<T> RunDirectlyOrDispatchAsync<T>(this CoreDispatcher dispatcher, Func<T> func, CancellationToken cancellationToken)
        {
            if (dispatcher.HasThreadAccess)
            {
                return func();
            }
            else
            {
                T result = default(T);
                await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => { result = func(); }).AsTask(cancellationToken).ConfigureAwait(false);
                return result;
            }
        }

        public static Task RunDirectlyOrDispatchAsync(this CoreDispatcher dispatcher, Func<CancellationToken, Task> func, CancellationToken cancellationToken)
        {
            return dispatcher.RunDirectlyOrDispatchAsync(async innerCt => { await func(innerCt).ConfigureAwait(false); return 0; }, cancellationToken);
        }

        public static async Task<T> RunDirectlyOrDispatchAsync<T>(this CoreDispatcher dispatcher, Func<CancellationToken, Task<T>> func, CancellationToken cancellationToken)
        {
            Task<T> task = null;

            if (dispatcher.HasThreadAccess)
            {
                task = func(cancellationToken);
            }
            else
            {
                await dispatcher.RunAsync(
                    CoreDispatcherPriority.Normal,
                    () =>
                    {
                        task = func(cancellationToken);
                    }).AsTask(cancellationToken).ConfigureAwait(false);
            }

            return await task.ConfigureAwait(false);
        }
    }
}
