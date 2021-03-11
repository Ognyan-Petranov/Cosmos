namespace Cosmos.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Cosmos.Web.ViewModels.Starships;

    public interface IStarshipsService
    {
        ICollection<StarshipViewModel> GetAllStarshipsByRace(string race);

        ICollection<StarshipViewModel> GetStarshipsPerPlayerById(string id);

        StarshipViewModel GetStarshipById(string id);

        Task BuyStarship(string playerData, string shipId);

        ICollection<StarshipViewModel> GetStarshipsPerPlayer(string playerId);
    }
}
