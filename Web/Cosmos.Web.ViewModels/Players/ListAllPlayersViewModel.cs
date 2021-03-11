namespace Cosmos.Web.ViewModels.Players
{
    using System.Collections.Generic;

    public class ListAllPlayersViewModel
    {
        public ICollection<PlayerViewModel> AllPlayers { get; set; }
    }
}
