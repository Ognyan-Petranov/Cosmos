namespace Cosmos.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Cosmos.Data.Models;
    using Cosmos.Web.ViewModels.Alliances;
    using Cosmos.Web.ViewModels.Players;

    public interface IAlliancesService
    {
        Task<string> CreateAllianceAsync(CreateAllianceInputModel input, string allianceLeader);

        ICollection<AllianceViewModel> GetAllAlliances();

        Task<AllianceViewModel> GetAllianceById(string allianceId);

        ICollection<PlayerViewModel> GetAllMembers(string allianceId);

        Task Join(string allianceId, string playerId);

        Task Leave(string allianceId, string playerId);

        Task Disband(string allianceId, string playerId);

        Task AddPlayerToAllianceAsync(string playerData, string allianceId);
    }
}
