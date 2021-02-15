using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Browse;

namespace FluentSpotifyApi.Builder.Browse
{
    /// <summary>
    /// The builder for "browse/new-releases" endpoint.
    /// </summary>
    public interface IBrowseNewReleasesBuilder
    {
        /// <summary>
        /// Gets a list of new album releases featured in Spotify(shown, for example, on a Spotify player’s “Browse” tab).
        /// </summary>
        /// <param name="country">
        /// A country: an ISO 3166-1 alpha-2 country code. Provide this parameter if you want the list of returned items to be relevant to a particular country.
        /// If omitted, the returned items will be relevant to all countries.</param>
        /// <param name="limit">The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">The index of the first item to return. Default: 0 (the first object). Use with <paramref name="limit"/> to get the next set of items.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<NewReleases> GetAsync(string country = null, int? limit = null, int? offset = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}
