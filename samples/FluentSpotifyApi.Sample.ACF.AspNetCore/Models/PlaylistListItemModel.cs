using System.ComponentModel.DataAnnotations;

namespace FluentSpotifyApi.Sample.ACF.AspNetCore.Models
{
    public class PlaylistListItemModel
    {
        [Display(Name = "ID")]
        public string Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Owner")]
        public string Owner { get; set; }

        [Display(Name = "No. of tracks")]
        public int NumberOfTracks { get; set; }

        [Display(Name = "Is Public")]
        public bool? IsPublic { get; set; }

        [Display(Name = "Is Collaborative")]
        public bool IsCollaborative { get; set; }

        [Display(Name = "Is Owned")]
        public bool IsOwned { get; set; }
    }
}
