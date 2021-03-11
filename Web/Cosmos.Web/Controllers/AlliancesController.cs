namespace Cosmos.Web.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Cosmos.Data.Common.Repositories;
    using Cosmos.Data.Models;
    using Cosmos.Services.Data;
    using Cosmos.Web.Infrastructure.Filters;
    using Cosmos.Web.ViewModels.Alliances;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;

    [Authorize]
    [TypeFilter(typeof(ValidatePlayerIsCreatedActionFilter))]
    public class AlliancesController : BaseController
    {
        private readonly IRepository<Player> playersRepository;
        private readonly IAlliancesService alliancesService;
        private readonly IPlayersService playersService;
        private readonly IImagesService imageFileServices;
        private readonly UserManager<ApplicationUser> userManager;

        public AlliancesController(IRepository<Player> playersRepository, IAlliancesService alliancesService, IPlayersService playersService, IImagesService imageFileServices, UserManager<ApplicationUser> userManager)
        {
            this.playersRepository = playersRepository;
            this.alliancesService = alliancesService;
            this.playersService = playersService;
            this.imageFileServices = imageFileServices;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAllianceInputModel input)
        {
            var applicationUserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var allianceLeader = this.playersService.GetPlayer(applicationUserId).PlayerName;

            var id = await this.alliancesService.CreateAllianceAsync(input, allianceLeader);

            return this.RedirectToAction("Home", new RouteValueDictionary(new { controller = "Alliances", action = "Home", Id = id }));
        }

        [HttpGet]
        public async Task<IActionResult> Home()
        {
            var id = this.playersService.GetPlayer(this.userManager.GetUserId(this.User)).AllianceId;
            if (id is null)
            {
                return this.RedirectToAction("All", "Alliances");
            }

            var model = await this.alliancesService.GetAllianceById(id);
            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            if (id is null)
            {
                id = this.playersService.GetPlayer(this.userManager.GetUserId(this.User)).AllianceId;
                if (id is null)
                {
                    return this.RedirectToAction("All", "Alliances");
                }
            }

            var model = await this.alliancesService.GetAllianceById(id);
            return this.View(model);
        }

        [HttpGet]
        public IActionResult All()
        {
            var viewModel = new ListAllAllianceViewModel
            {
                AllAlliances = this.alliancesService.GetAllAlliances(),
            };
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Join(string id)
        {
            var applicationUserId = this.userManager.GetUserId(this.User);
            var playerId = this.playersService.GetPlayerId(applicationUserId);
            var playerName = this.playersRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == playerId).PlayerName;

            if (this.playersService.CheckIfPlayerIsInAlliance(playerName))
            {
                return this.RedirectToAction("Error");
            }

            await this.alliancesService.AddPlayerToAllianceAsync(playerId, id);
            return this.RedirectToAction("Home", new RouteValueDictionary(new { controller = "Alliances", action = "Home", Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> Leave(string id)
        {
            var applicationUserId = this.userManager.GetUserId(this.User);
            var playerId = this.playersService.GetPlayerId(applicationUserId);
            var playerName = this.playersRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == playerId).PlayerName;

            if (!this.playersService.CheckIfPlayerIsInAlliance(playerName))
            {
                return this.RedirectToAction("Error");
            }

            await this.alliancesService.Leave(id, playerId);
            return this.RedirectToAction("All", "Alliances");
        }

        [HttpPost]
        public async Task<IActionResult> Disband(string id)
        {
            string applicationUserId = this.userManager.GetUserId(this.User);
            var playerId = this.playersService.GetPlayerId(applicationUserId);
            var playerName = this.playersRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == playerId).PlayerName;

            if (!this.playersService.CheckIfPlayerIsLeader(playerName, id))
            {
                return this.RedirectToAction("Error");
            }

            await this.alliancesService.Disband(id, playerId);

            return this.RedirectToAction("All", "Alliances");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeAllianceImage(IFormFile newImage, string id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("Error");
            }

            string applicationUserId = this.userManager.GetUserId(this.User);
            var playerId = this.playersService.GetPlayerId(applicationUserId);
            var playerName = this.playersRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == playerId).PlayerName;

            if (!this.playersService.CheckIfPlayerIsLeader(playerName, id))
            {
                return this.RedirectToAction("Error");
            }

            await this.imageFileServices.AddImage(newImage.OpenReadStream(), id);
            return this.RedirectToAction("Home", new RouteValueDictionary(new { controller = "Alliances", action = "Home", Id = id }));
        }
    }
}
