using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FluentSpotifyApi.Sample.ACF.AspNetCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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

        public IActionResult Privacy()
        {
            return this.View();
        }

        public IActionResult UserProfile()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }

        [Authorize]
        public async Task<IActionResult> Playlists()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var playlists = (await this.fluentSpotifyClient.Me.Playlists.GetAsync(limit: 20, offset: 0));
            var model = playlists
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
