namespace Cosmos.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Cosmos.Data.Common.Repositories;
    using Cosmos.Data.Models;
    using Cosmos.Services.Mapping;
    using Cosmos.Web.ViewModels.Players;

    public class PlayersService : IPlayersService
    {
        private readonly IRepository<Player> playerRepository;
        private readonly IImagesService imagesService;
        private readonly IRepository<Alliance> alliancesRepository;

        public PlayersService(IRepository<Player> playerRepository, IImagesService imagesService, IRepository<Alliance> alliancesRepository)
        {
            this.playerRepository = playerRepository;
            this.imagesService = imagesService;
            this.alliancesRepository = alliancesRepository;
        }

        // TODO: Write logic for attacks.
        public void Attack(string targetId)
        {
            throw new NotImplementedException();
        }

        // TODO: Add constants for the magic numbers below.
        public async Task CreatePlayerAsync(CreatePlayerInputModel input, string applicationUserId)
        {
            if (this.IsPlayerCreated(applicationUserId))
            {
                throw new InvalidOperationException("Only one player per account is allowed!");
            }

            var player = new Player
            {
                PlayerName = input.PlayerName,
                Experience = 0,
                Money = 50_000,
                CreatedOn = DateTime.UtcNow,
                RaceId = input.RaceId,
                ApplicationUserId = applicationUserId,
                IsDeleted = false,
                RankId = "c907502b-94f7-403f-903b-a3f345561b9c",
            };

            await this.playerRepository.AddAsync(player);
            await this.playerRepository.SaveChangesAsync();
        }

        public Player GetPlayer(string applicationUserId) => this.playerRepository.AllAsNoTracking().FirstOrDefault(x => x.ApplicationUserId == applicationUserId);

        public string GetPlayerId(string applicationUserId) => this.playerRepository.AllAsNoTracking().FirstOrDefault(x => x.ApplicationUserId == applicationUserId)?.Id;

        public bool IsPlayerCreated(string applicationUserId) => this.playerRepository.AllAsNoTracking().Any(x => x.ApplicationUser.Id == applicationUserId);

        public PlayerViewModel GetPlayerById(string playerId) => this.playerRepository.AllAsNoTracking().Where(x => x.Id == playerId).To<PlayerViewModel>().FirstOrDefault();

        public ICollection<PlayerViewModel> GetAllPlayers() => this.playerRepository.AllAsNoTracking().To<PlayerViewModel>().ToList();

        public async Task<PlayerViewModel> GetPlayerByApplicationUserId(string playerId)
        {
            var player = this.playerRepository.AllAsNoTracking().Where(x => x.ApplicationUserId == playerId).To<PlayerViewModel>().FirstOrDefault();
            var playerImageId = this.playerRepository.AllAsNoTracking().FirstOrDefault(x => x.ApplicationUserId == playerId)?.ImageId;

            if (player == null)
            {
                return player;
            }

            if (playerImageId != null)
            {
                player.Image = await this.imagesService.GetImageData(playerImageId);
            }

            return player;
        }

        public bool CheckIfPlayerIsInAlliance(string playerName) => this.playerRepository.AllAsNoTracking().Where(x => x.AllianceId != null).Any(x => x.PlayerName == playerName);

        public bool CheckIfPlayerIsLeader(string playerName, string allianceId)
        {
            var player = this.playerRepository.AllAsNoTracking().FirstOrDefault(x => x.PlayerName == playerName);
            var allianceLeader = this.alliancesRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == allianceId)?.AllianceLeader;

            if (allianceId != null && player != null && player.AllianceId == allianceId && player.PlayerName == allianceLeader)
            {
                return true;
            }

            return false;
        }
    }
}
