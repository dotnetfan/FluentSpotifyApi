using System;
using System.Threading;
using System.Threading.Tasks;

namespace FluentSpotifyApi.Core.User
{
    /// <summary>
    /// The default implementation of <see cref="ICurrentUserProvider"/>.
    /// </summary>
    public class NotSupportedCurrentUserProvider : ICurrentUserProvider
    {
        /// <inheritdoc />
        public Task<IUser> GetAsync(CancellationToken cancellationToken)
        {
            throw new NotSupportedException("No Spotify authorization flow is used. Authenticated user is not available.");
        }
    }
}
