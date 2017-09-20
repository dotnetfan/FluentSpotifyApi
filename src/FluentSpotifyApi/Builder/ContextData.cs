using FluentSpotifyApi.Client;
using FluentSpotifyApi.Core.Options;
using FluentSpotifyApi.Options;

namespace FluentSpotifyApi.Builder
{
    internal class ContextData
    {
        public ContextData(ISpotifyHttpClient spotifyHttpClient, IOptionsProvider<FluentSpotifyClientOptions> fluentSpotifyClientOptionsProvider) 
        {
            this.SpotifyHttpClient = spotifyHttpClient;
            this.FluentSpotifyClientOptionsProvider = fluentSpotifyClientOptionsProvider;
        }

        public ISpotifyHttpClient SpotifyHttpClient { get; }

        public IOptionsProvider<FluentSpotifyClientOptions> FluentSpotifyClientOptionsProvider { get; }
    }
}
