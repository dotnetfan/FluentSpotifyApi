using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Model;
using FluentSpotifyApi.Model.Episodes;

namespace FluentSpotifyApi.Builder.Shows
{
    /// <summary>
    /// The builder for "shows/{id}/episodes" endpoint.
    /// </summary>
    public interface IShowEpisodesBuilder
    {
        /// <summary>
        /// Get Spotify catalog information about an show’s episodes. Optional parameters can be used to limit the number of episodes returned.
        /// </summary>
        /// <param name="market">
        /// An ISO 3166-1 alpha-2 country code. If a country code is specified, only shows and episodes that are available in that market will be returned.
        /// If a valid user access token is specified in the request header, the country associated with the user account will take priority over this parameter.
        /// </param>
        /// <param name="limit">The maximum number of episodes to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">The index of the first episode to return. Default: 0 (the first object). Use with <paramref name="limit"/> to get the next set of episodes.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<Page<SimplifiedEpisode>> GetAsync(string market = null, int? limit = null, int? offset = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}
