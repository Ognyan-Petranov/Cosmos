namespace Cosmos.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Cosmos.Data.Common.Repositories;
    using Cosmos.Data.Models;
    using Cosmos.Services.Mapping;
    using Cosmos.Web.ViewModels.Alliances;
    using Cosmos.Web.ViewModels.Players;

    public class AlliancesService : IAlliancesService
    {
        private readonly IPlayersService playersService;
        private readonly IImagesService imagesService;
        private readonly IRepository<Alliance> alliancesRepository;
        private readonly IRepository<Player> playersRepository;

        public AlliancesService(IPlayersService playersService, IImagesService imageFileServices, IRepository<Alliance> alliancesRepository, IRepository<Player> playersRepository)
        {
            this.playersService = playersService;
            this.imagesService = imageFileServices;
            this.alliancesRepository = alliancesRepository;
            this.playersRepository = playersRepository;
        }

        public async Task<string> CreateAllianceAsync(CreateAllianceInputModel input, string allianceLeader)
        {
            if (this.alliancesRepository.AllAsNoTracking().Any(x => x.Name == input.Name))
            {
                throw new InvalidOperationException("Alliance name already taken!");
            }

            if (this.playersService.CheckIfPlayerIsInAlliance(allianceLeader))
            {
                throw new InvalidOperationException("You can only be member of one alliance!");
            }

            var alliance = new Alliance
            {
                Name = input.Name,
                Description = input.Description,
                AllianceLeader = allianceLeader,
            };

            await this.alliancesRepository.AddAsync(alliance);
            await this.alliancesRepository.SaveChangesAsync();

            if (!(input.AllianceImage?.Length is null))
            {
                await this.imagesService.AddImage(input.AllianceImage.OpenReadStream(), alliance.Id);
            }

            await this.AddPlayerToAllianceAsync(allianceLeader, alliance.Id);
            return alliance.Id;
        }

        // TODO: Add validations who can disband alliance.(Only AL)
        public async Task Disband(string id, string playerId)
        {
            var alliance = this.alliancesRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == id);

            if (alliance == null)
            {
                throw new ArgumentException("Invalid alliance!");
            }

            await this.Leave(id, playerId);
            this.alliancesRepository.Delete(alliance);
            await this.alliancesRepository.SaveChangesAsync();
        }

        public ICollection<PlayerViewModel> GetAllMembers(string allianceId)
        {
            return this.playersRepository.AllAsNoTracking().Where(x => x.AllianceId == allianceId).Select(x => x).To<PlayerViewModel>().ToList();
        }

        public ICollection<AllianceViewModel> GetAllAlliances()
        {
            return this.alliancesRepository.AllAsNoTracking().Select(x => x).To<AllianceViewModel>().ToList();
        }

        public async Task Leave(string allianceId, string playerId)
        {
            var player = this.playersRepository.All().FirstOrDefault(x => x.Id == playerId);

            if (player.AllianceId == allianceId)
            {
                player.AllianceId = null;
                this.playersRepository.Update(player);
                await this.playersRepository.SaveChangesAsync();
            }
        }

        public async Task Join(string allianceId, string playerId)
        {
            await this.AddPlayerToAllianceAsync(playerId, allianceId);
        }

        public async Task<AllianceViewModel> GetAllianceById(string id)
        {
            var alliance = this.alliancesRepository.AllAsNoTracking().Where(x => x.Id == id).To<AllianceViewModel>().FirstOrDefault();
            var allianceImageId = this.alliancesRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == id).ImageId;
            if (alliance == null)
            {
                throw new ArgumentException("Invalid alliance!");
            }

            if (allianceImageId != null)
            {
                alliance.RenderImage = await this.imagesService.GetImageData(allianceImageId);
            }

            alliance.Players = this.GetAllMembers(id);
            return alliance;
        }

        public async Task AddPlayerToAllianceAsync(string playerData, string allianceId)
        {
            var player = this.playersRepository.All().FirstOrDefault(x => x.PlayerName == playerData || x.Id == playerData);
            player.AllianceId = allianceId;
            this.playersRepository.Update(player);
            await this.playersRepository.SaveChangesAsync();
        }
    }
}
