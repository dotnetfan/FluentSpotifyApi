using System.ComponentModel.DataAnnotations;

namespace FluentSpotifyApi.Sample.CCF.AspNetCore.Models
{
    public class AlbumItemModel
    {
        [Display(Name = "ID")]
        public string Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Artists")]
        public string Artists { get; set; }
    }
}
