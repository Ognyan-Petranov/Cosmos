namespace Cosmos.Web.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using Cosmos.Data.Common.Repositories;
    using Cosmos.Data.Models;
    using Cosmos.Services.Data;
    using Cosmos.Web.Infrastructure.Filters;
    using Cosmos.Web.ViewModels.Messages;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    [TypeFilter(typeof(ValidatePlayerIsCreatedActionFilter))]
    public class MessagesController : BaseController
    {
        private readonly IPlayersService playersService;
        private readonly IRepository<Player> playersRepository;
        private readonly IMessagesService messagesService;
        private readonly UserManager<ApplicationUser> userManager;

        public MessagesController(IPlayersService playersService, IRepository<Player> playersRepository, IMessagesService messagesService, UserManager<ApplicationUser> userManager)
        {
            this.playersService = playersService;
            this.playersRepository = playersRepository;
            this.messagesService = messagesService;
            this.userManager = userManager;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public IActionResult Send()
        {
            return this.View();
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<IActionResult> Send(MessageInputModel input)
        {
            //var players = this.playersService.GetAllPlayers().Select(x => x.PlayerName);

            //if (!string.IsNullOrEmpty(input.ReceiverName))
            //{
            //    players = players.Where(x => x.Contains(input.ReceiverName));
            //}

            var applicationUserId = this.userManager.GetUserId(this.User);
            var userId = this.playersService.GetPlayerId(applicationUserId);
            await this.messagesService.SendMessage(input, userId);
            return this.RedirectToAction("Send", "Messages");
        }

        public IActionResult All()
        {
            var applicationUserId = this.userManager.GetUserId(this.User);
            var userId = this.playersService.GetPlayerId(applicationUserId);
            ListAllMessagesViewModel model = new ListAllMessagesViewModel
            {
                AllMessages = this.messagesService.ViewAllMessages(userId),
            };
            return this.View(model);
        }

        public IActionResult Read(string id)
        {
            var message = this.messagesService.GetMessageById(id);
            return this.View(message);
        }
    }
}
