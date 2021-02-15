using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Shows;

namespace FluentSpotifyApi.Builder.Shows
{
    /// <summary>
    /// The builder for "shows?ids={ids}" endpoint.
    /// </summary>
    public interface IShowsBuilder
    {
        /// <summary>
        /// Gets Spotify catalog information for several shows based on their Spotify IDs.
        /// </summary>
        /// <param name="market">
        /// An ISO 3166-1 alpha-2 country code. If a country code is specified, only shows and episodes that are available in that market will be returned.
        /// If a valid user access token is specified in the request header, the country associated with the user account will take priority over this parameter.
        /// </param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<SimplifiedShowsResponse> GetAsync(string market = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}
