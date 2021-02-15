using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Episodes;

namespace FluentSpotifyApi.Builder.Episodes
{
    /// <summary>
    /// The builder for "episodes?ids={ids}" endpoint.
    /// </summary>
    public interface IEpisodesBuilder
    {
        /// <summary>
        /// Gets Spotify catalog information for several episodes based on their Spotify IDs.
        /// </summary>
        /// <param name="market">
        /// An ISO 3166-1 alpha-2 country code. If a country code is specified, only shows and episodes that are available in that market will be returned.
        /// If a valid user access token is specified in the request header, the country associated with the user account will take priority over this parameter.
        /// </param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<EpisodesResponse> GetAsync(string market = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}
