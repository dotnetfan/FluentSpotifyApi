using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model.Episodes;

namespace FluentSpotifyApi.Builder.Episodes
{
    /// <summary>
    /// The builder for "episodes/{id}" endpoint.
    /// </summary>
    public interface IEpisodeBuilder
    {
        /// <summary>
        /// Gets Spotify catalog information for a single episode identified by its unique Spotify ID.
        /// </summary>
        /// <param name="market">
        /// An ISO 3166-1 alpha-2 country code. If a country code is specified, only shows and episodes that are available in that market will be returned.
        /// If a valid user access token is specified in the request header, the country associated with the user account will take priority over this parameter.
        /// </param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task<Episode> GetAsync(string market = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}
