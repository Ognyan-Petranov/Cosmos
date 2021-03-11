namespace Cosmos.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Cosmos.Data.Models;
    using Cosmos.Web.ViewModels.Players;

    public interface IPlayersService
    {
        Task CreatePlayerAsync(CreatePlayerInputModel input, string applicationUserId);

        string GetPlayerId(string applicationUserId);

        Player GetPlayer(string applicationUserId);

        void Attack(string targetId);

        bool IsPlayerCreated(string applicationUserId);

        PlayerViewModel GetPlayerById(string playerId);

        ICollection<PlayerViewModel> GetAllPlayers();

        Task<PlayerViewModel> GetPlayerByApplicationUserId(string playerId);

        bool CheckIfPlayerIsInAlliance(string playerName);

        bool CheckIfPlayerIsLeader(string playerName, string allianceId);
    }
}
