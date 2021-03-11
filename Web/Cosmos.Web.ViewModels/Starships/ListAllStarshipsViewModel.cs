namespace Cosmos.Web.ViewModels.Starships
{
    using System.Collections.Generic;

    public class ListAllStarshipsViewModel
    {
        public ICollection<StarshipViewModel> AllShips { get; set; }
    }
}
