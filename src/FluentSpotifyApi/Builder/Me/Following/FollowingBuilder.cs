using FluentSpotifyApi.Model.Following;

namespace FluentSpotifyApi.Builder.Me.Following
{
    internal class FollowingBuilder : BuilderBase, IFollowingBuilder
    {
        public FollowingBuilder(BuilderBase parent)
            : base(parent)
        {
        }

        public IRetrievableFollowedItemsBuilder<FollowedArtists> Artists
            => new RetrievableFollowedItemsBuilder<FollowedArtists>(this, "artist");

        public IFollowedItemsBuilder Users
            => new FollowedItemsBuilder(this, "user");

        public IFollowedPlaylistBuilder Playlists
            => new FollowedPlaylistBuilder(this.CreateRootBuilder());
    }
}
