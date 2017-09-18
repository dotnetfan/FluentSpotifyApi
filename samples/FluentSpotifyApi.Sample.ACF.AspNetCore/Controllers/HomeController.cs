using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FluentSpotifyApi.AuthorizationFlows.AspNetCore.AuthorizationCode.Extensions;
using FluentSpotifyApi.Sample.ACF.AspNetCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FluentSpotifyApi.Sample.ACF.AspNetCore.Controllers
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

        [Authorize]
        public async Task<IActionResult> Playlists()
        {
            var userId = this.User.GetNameIdentifier();

            var model = (await this.fluentSpotifyClient.Me.Playlists.GetAsync(limit: 20, offset: 0))
                .Items
                .Select(item => new PlaylistListItemModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Owner = item.Owner?.DisplayName ?? item.Owner?.Id,
                    NumberOfTracks = (item.Tracks?.Total).GetValueOrDefault(),
                    IsPublic = item.Public,
                    IsCollaborative = item.Collaborative,
                    IsOwned = item.Owner?.Id == userId
                }).ToList();

            return this.View(model);
        }
    }
}
