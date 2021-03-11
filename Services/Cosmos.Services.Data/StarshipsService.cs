namespace Cosmos.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Cosmos.Data.Common.Repositories;
    using Cosmos.Data.Models;
    using Cosmos.Services.Mapping;
    using Cosmos.Web.ViewModels.Starships;

    public class StarshipsService : IStarshipsService
    {
        private readonly IRepository<Starship> starshipsRepository;
        private readonly IRepository<Player> playersRepository;
        private readonly IRepository<PlayerStarship> playerStarshipRepository;

        public StarshipsService(IRepository<Starship> starshipsRepository, IRepository<Player> playersRepository, IRepository<PlayerStarship> playerStarshipRepository)
        {
            this.starshipsRepository = starshipsRepository;
            this.playersRepository = playersRepository;
            this.playerStarshipRepository = playerStarshipRepository;
        }

        public ICollection<StarshipViewModel> GetAllStarshipsByRace(string race)
        {
            return this.starshipsRepository.AllAsNoTracking().Where(x => x.Race.Name == race).To<StarshipViewModel>().ToList();
        }

        public StarshipViewModel GetStarshipById(string id)
        {
            return this.starshipsRepository.AllAsNoTracking().Where(x => x.Id == id)?.To<StarshipViewModel>().FirstOrDefault();
        }

        public ICollection<StarshipViewModel> GetStarshipsPerPlayerById(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task BuyStarship(string playerData, string shipId)
        {
            var starship = this.starshipsRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == shipId);
            var player = this.playersRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == playerData);

            await this.playerStarshipRepository.AddAsync(new PlayerStarship { StarshipId = starship.Id, PlayerId = player.Id });
            await this.playerStarshipRepository.SaveChangesAsync();
        }

        public ICollection<StarshipViewModel> GetStarshipsPerPlayer(string playerId)
        {
            return this.playerStarshipRepository.AllAsNoTracking().Where(x => x.PlayerId == playerId).Select(x => x.Starship).To<StarshipViewModel>().ToList();
        }
    }
}
