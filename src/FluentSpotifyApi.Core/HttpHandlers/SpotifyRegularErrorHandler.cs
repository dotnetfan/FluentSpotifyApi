using System.Net.Http;
using FluentSpotifyApi.Core.Exceptions;
using FluentSpotifyApi.Core.Model;

namespace FluentSpotifyApi.Core.HttpHandlers
{
    /// <summary>
    /// The <see cref="DelegatingHandler"/> that shields exceptions thrown by <see cref="HttpClient"/>
    /// and transforms error responses with regular error content into <see cref="SpotifyRegularErrorException"/>.
    /// </summary>
    /// <typeparam name="TClient"></typeparam>
    /// <seealso cref="SpotifyErrorHandler{TClient,RegularErrorResponse,RegularError}" />
    public class SpotifyRegularErrorHandler<TClient> : SpotifyErrorHandler<TClient, RegularErrorResponse, RegularError>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyRegularErrorHandler{TClient}" /> class.
        /// </summary>
        public SpotifyRegularErrorHandler()
            : base(r => r.Error, (t, s, c, e) => new SpotifyRegularErrorException(t, s, c, e))
        {
        }
    }
}
