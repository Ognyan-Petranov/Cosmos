namespace Cosmos.Web.Controllers
{
    using System.Threading.Tasks;
    using Cosmos.Data.Models;
    using Cosmos.Services.Data;
    using Cosmos.Web.Infrastructure.Filters;
    using Cosmos.Web.ViewModels.Starships;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    [TypeFilter(typeof(ValidatePlayerIsCreatedActionFilter))]
    public class StarshipsController : Controller
    {
        private readonly IStarshipsService starshipsService;
        private readonly IPlayersService playersService;
        private readonly UserManager<ApplicationUser> userManager;

        public StarshipsController(IStarshipsService starshipsService, IPlayersService playersService, UserManager<ApplicationUser> userManager)
        {
            this.starshipsService = starshipsService;
            this.playersService = playersService;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Dealer()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult ByRace(string race)
        {
            var model = new ListAllStarshipsViewModel();
            model.AllShips = this.starshipsService.GetAllStarshipsByRace(race);
            return this.View(model);
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            var model = this.starshipsService.GetStarshipById(id);
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Buy(string id)
        {
            var applicationUserId = this.userManager.GetUserId(this.User);
            var playerId = this.playersService.GetPlayerId(applicationUserId);
            await this.starshipsService.BuyStarship(playerId, id);

            return this.RedirectToAction("Own", "Starships");
        }

        [HttpGet]
        public IActionResult Own()
        {
            var applicationUserId = this.userManager.GetUserId(this.User);
            var playerId = this.playersService.GetPlayerId(applicationUserId);
            var model = new ListAllStarshipsViewModel { AllShips = this.starshipsService.GetStarshipsPerPlayer(playerId) };
            return this.View(model);
        }
    }
}
