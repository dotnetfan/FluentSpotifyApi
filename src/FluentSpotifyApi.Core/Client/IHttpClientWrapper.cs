using System.Threading;
using System.Threading.Tasks;

namespace FluentSpotifyApi.Core.Client
{
    /// <summary>
    /// Wraps <see cref="System.Net.Http.HttpClient"/> instance.
    /// </summary>
    public interface IHttpClientWrapper
    {
        /// <summary>
        /// Sends request to the server.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="httpRequest">The HTTP request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<TResult> SendAsync<TResult>(HttpRequest<TResult> httpRequest, CancellationToken cancellationToken);
    }
}
