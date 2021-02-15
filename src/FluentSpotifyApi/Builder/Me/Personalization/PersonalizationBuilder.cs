using FluentSpotifyApi.Model.Artists;
using FluentSpotifyApi.Model.Tracks;

namespace FluentSpotifyApi.Builder.Me.Personalization
{
    internal class PersonalizationBuilder : BuilderBase, IPersonalizationBuilder
    {
        public PersonalizationBuilder(BuilderBase parent)
            : base(parent)
        {
        }

        public ITopBuilder<Artist> TopArtists => new TopBuilder<Artist>(this, "artists");

        public ITopBuilder<Track> TopTracks => new TopBuilder<Track>(this, "tracks");
    }
}
