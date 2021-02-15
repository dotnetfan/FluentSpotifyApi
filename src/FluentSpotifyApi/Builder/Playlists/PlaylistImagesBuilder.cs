using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentSpotifyApi.Core.Model;
using FluentSpotifyApi.Core.Utils;
using FluentSpotifyApi.Extensions;

namespace FluentSpotifyApi.Builder.Playlists
{
    internal class PlaylistImagesBuilder : BuilderBase, IPlaylistImagesBuilder
    {
        public PlaylistImagesBuilder(BuilderBase parent)
            : base(parent, "images".Yield())
        {
        }

        public Task<Image[]> GetAsync(CancellationToken cancellationToken)
            => this.GetAsync<Image[]>(cancellationToken);

        public async Task UpdateAsync(byte[] jpegCover, CancellationToken cancellationToken)
        {
            SpotifyArgumentAssertUtils.ThrowIfNull(jpegCover, nameof(jpegCover));

            await this.SendJpegAsync(HttpMethod.Put, jpegCover, cancellationToken).ConfigureAwait(false);
        }
    }
}
