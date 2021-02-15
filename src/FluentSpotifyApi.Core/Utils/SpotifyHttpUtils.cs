using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Core.Exceptions;

namespace FluentSpotifyApi.Core.Utils
{
    /// <summary>
    /// The set of HTTP utilities intended for FluentSpotifyApi library usage.
    /// </summary>
    public static class SpotifyHttpUtils
    {
        /// <summary>
        /// Shields <see cref="OperationCanceledException"/> thrown by <see cref="HttpClient"/> that represents client timeout.
        /// Unfortunately this is not possible to do from <see cref="DelegatingHandler"/> because inside handler external and internal cancellation tokens are merged together.
        /// </summary>
        /// <typeparam name="TClient">The client type.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public static Task HandleTimeoutAsync<TClient>(Func<CancellationToken, Task> action, CancellationToken cancellationToken)
        {
            return HandleTimeoutAsync<TClient, int>(async (innerCt) => { await action(innerCt).ConfigureAwait(false); return 0; }, cancellationToken);
        }

        /// <summary>
        /// Shields <see cref="OperationCanceledException"/> thrown by <see cref="HttpClient"/> that represents client timeout.
        /// Unfortunately this is not possible to do from <see cref="DelegatingHandler"/> because inside handler external and internal cancellation tokens are merged together.
        /// </summary>
        /// <typeparam name="TClient">The client type.</typeparam>
        /// <typeparam name="T">The return type.</typeparam>
        /// <param name="func">The function.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public static async Task<T> HandleTimeoutAsync<TClient, T>(Func<CancellationToken, Task<T>> func, CancellationToken cancellationToken)
        {
            try
            {
                return await func(cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException e) when (!cancellationToken.IsCancellationRequested)
            {
                throw new SpotifyHttpClientTimeoutException(typeof(TClient), e);
            }
        }

        /// <summary>
        /// Gets Spotify HTTP client name.
        /// </summary>
        /// <typeparam name="TClient">The client type.</typeparam>
        /// <returns></returns>
        public static string GetClientName<TClient>() => $"FluentSpotifyApi.{typeof(TClient).Name}";
    }
}
