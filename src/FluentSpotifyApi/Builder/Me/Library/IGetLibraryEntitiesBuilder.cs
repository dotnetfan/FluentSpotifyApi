using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model;

namespace FluentSpotifyApi.Builder.Me.Library
{
    /// <summary>
    /// The builder for "me/entities" endpoint.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGetLibraryEntitiesBuilder<T>
    {
        /// <summary>
        /// Get a list of the songs or albums saved in the current Spotify user’s “Your Music” library.
        /// </summary>
        /// <param name="limit">The maximum number of objects to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">The index of the first object to return. Default: 0 (i.e., the first object).</param>
        /// <param name="market">An ISO 3166-1 alpha-2 country code.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<Page<T>> GetAsync(int limit = 20, int offset = 0, string market = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}
