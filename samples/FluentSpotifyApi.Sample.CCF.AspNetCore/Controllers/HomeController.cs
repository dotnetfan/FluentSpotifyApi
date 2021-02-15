using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FluentSpotifyApi.Sample.CCF.AspNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
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
