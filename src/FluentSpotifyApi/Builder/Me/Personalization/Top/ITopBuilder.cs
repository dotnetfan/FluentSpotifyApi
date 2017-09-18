using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model;

namespace FluentSpotifyApi.Builder.Me.Personalization.Top
{
    /// <summary>
    /// The builder for "me/top/entity" endpoint.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITopBuilder<T>
    {
        /// <summary>
        /// Get the current user’s top artists or tracks based on calculated affinity.
        /// </summary>
        /// <param name="limit">The number of entities to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">The index of the first entity to return. Default: 0 (i.e., the first track).</param>
        /// <param name="timeRange">Over what time frame the affinities are computed. Default: <see cref="TimeRange.MediumTerm"/>.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<Page<T>> GetAsync(int limit = 20, int offset = 0, TimeRange timeRange = TimeRange.MediumTerm, CancellationToken cancellationToken = default(CancellationToken));
    }
}
