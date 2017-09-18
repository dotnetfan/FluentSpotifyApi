using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FluentSpotifyApi.Sample.CCF.AspNetCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace FluentSpotifyApi.Sample.CCF.AspNetCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFluentSpotifyClient fluentSpotifyClient;

        public HomeController(IFluentSpotifyClient fluentSpotifyClient)
        {
            this.fluentSpotifyClient = fluentSpotifyClient;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult About()
        {
            this.ViewData["Message"] = "Your application description page.";

            return this.View();
        }

        public IActionResult Contact()
        {
            this.ViewData["Message"] = "Your contact page.";

            return this.View();
        }

        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> NewReleases()
        {
            var model = (await this.fluentSpotifyClient.Browse.NewReleases.GetAsync(limit: 20, offset: 0)).Albums.Items.Select(item => new AlbumItemModel
            {
                Id = item.Id,
                Name = item.Name,
                Artists = string.Join(", ", item.Artists.Select(artist => artist.Name))
            }).ToList();

            return this.View(model);
        }
    }
}
