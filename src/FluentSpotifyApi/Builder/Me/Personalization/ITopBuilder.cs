using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model;

namespace FluentSpotifyApi.Builder.Me.Personalization
{
    /// <summary>
    /// The builder for "me/top/{entity}" endpoint.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITopBuilder<T>
    {
        /// <summary>
        /// Gets the current user’s top artists or tracks based on calculated affinity.
        /// </summary>
        /// <param name="timeRange">Over what time frame the affinities are computed. Default: <see cref="TimeRange.MediumTerm"/>.</param>
        /// <param name="limit">The number of entities to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">The index of the first entity to return. Default: 0 (i.e., the first track). Use with <paramref name="offset"/> to get the next set of entities.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<Page<T>> GetAsync(TimeRange? timeRange = null, int? limit = null, int? offset = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}
