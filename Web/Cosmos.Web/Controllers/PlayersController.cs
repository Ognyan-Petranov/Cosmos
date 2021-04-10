namespace Cosmos.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Cosmos.Data.Models;
    using Cosmos.Services.Data;
    using Cosmos.Web.Infrastructure.Filters;
    using Cosmos.Web.ViewModels.Players;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;

    [Authorize]

    public class PlayersController : BaseController
    {
        private readonly IRacesService racesService;
        private readonly IPlayersService playersService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IImagesService imagesService;

        public PlayersController(IRacesService racesService, IPlayersService playersService, UserManager<ApplicationUser> userManager, IImagesService imagesService)
        {
            this.racesService = racesService;
            this.playersService = playersService;
            this.userManager = userManager;
            this.imagesService = imagesService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new CreatePlayerInputModel
            {
                Races = this.racesService.GetAllRacesAsKeyValuePairs(),
            };
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePlayerInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("Error");
            }

            var applicationUserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await this.playersService.CreatePlayerAsync(input, applicationUserId);
            return this.Redirect("/");
        }

        [HttpGet]
        [TypeFilter(typeof(ValidatePlayerIsCreatedActionFilter))]
        public IActionResult Details(string id)
        {
            var model = this.playersService.GetPlayerById(id);
            return this.View(model);
        }

        [HttpGet]
        // [TypeFilter(typeof(ValidatePlayerIsCreatedActionFilter))]
        public async Task<IActionResult> All()
        {
            var model = new ListAllPlayersViewModel
            {
                AllPlayers = this.playersService.GetAllPlayers(),
            };
            if (model.AllPlayers.Count != 0)
            {
                foreach (var player in model.AllPlayers)
                {
                    player.Image = await this.imagesService.GetImageData(player.ImageId);
                }
            }

            return this.View(model);
        }

        [HttpGet]
        [TypeFilter(typeof(ValidatePlayerIsCreatedActionFilter))]
        public async Task<IActionResult> Profile()
        {
            var applicationUserId = this.userManager.GetUserId(this.User);
            var model = await this.playersService.GetPlayerByApplicationUserId(applicationUserId);
            return this.View(model);
        }

        [HttpPost]
        [TypeFilter(typeof(ValidatePlayerIsCreatedActionFilter))]
        public async Task<IActionResult> ChangeProfilePicture(IFormFile image, string id)
        {
            try
            {
            await this.imagesService.AddImage(image.OpenReadStream(), id);
            }
            catch
            {
            }

            return this.RedirectToAction("Profile", new RouteValueDictionary(new { controller = "Players", action = "Profile", Id = id }));
        }
    }
}
