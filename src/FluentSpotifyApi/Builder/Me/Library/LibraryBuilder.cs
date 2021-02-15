using FluentSpotifyApi.Model.Library;

namespace FluentSpotifyApi.Builder.Me.Library
{
    internal class LibraryBuilder : BuilderBase, ILibraryBuilder
    {
        public LibraryBuilder(BuilderBase parent)
            : base(parent)
        {
        }

        public ILibraryItemsBuilder<SavedAlbum> Albums => new LibraryItemsBuilder<SavedAlbum>(this, "albums");

        public ILibraryItemsBuilder<SavedTrack> Tracks => new LibraryItemsBuilder<SavedTrack>(this, "tracks");

        public ILibraryItemsBuilder<SavedShow> Shows => new LibraryItemsBuilder<SavedShow>(this, "shows");
    }
}
