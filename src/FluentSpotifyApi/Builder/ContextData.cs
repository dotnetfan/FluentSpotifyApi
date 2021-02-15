using FluentSpotifyApi.Core.User;

namespace FluentSpotifyApi.Builder
{
    internal class ContextData
    {
        public ContextData(IFluentSpotifyHttpClientFactory httpClientFactory, ICurrentUserProvider currentUserProvider)
        {
            this.HttpClientFactory = httpClientFactory;
            this.CurrentUserProvider = currentUserProvider;
        }

        public IFluentSpotifyHttpClientFactory HttpClientFactory { get; }

        public ICurrentUserProvider CurrentUserProvider { get; }
    }
}
