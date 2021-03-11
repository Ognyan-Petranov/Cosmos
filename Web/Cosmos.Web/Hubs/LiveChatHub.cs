namespace Cosmos.Web.Hubs
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Cosmos.Data.Models;
    using Cosmos.Services.Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.SignalR;

    public class LiveChatHub : Hub, IUserIdProvider
    {
        private readonly IPlayersService playersService;
        private readonly UserManager<ApplicationUser> userManager;

        public LiveChatHub(IPlayersService playersService, UserManager<ApplicationUser> userManager)
        {
            this.playersService = playersService;
            this.userManager = userManager;
        }

        public string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.FindFirst(ClaimTypes.Email)?.Value;
        }

        [Authorize]
        public async Task Send(string message)
        {
            var applicationUserId = this.Context.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var playerName = this.playersService.GetPlayer(applicationUserId).PlayerName;
            await this.Clients.All.SendAsync(
                "NewMessage",
                new ChatMessage { User = playerName, Text = message, });
        }
    }
}
