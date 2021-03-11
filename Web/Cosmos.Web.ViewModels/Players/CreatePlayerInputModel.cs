namespace Cosmos.Web.ViewModels.Players
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Cosmos.Data.Models;
    using Cosmos.Services.Mapping;

    public class CreatePlayerInputModel : IMapTo<Player>
    {
        [Required]
        [StringLength(30)]
        public string PlayerName { get; set; }

        [Required]
        public string RaceId { get; set; }

        public string ApplicationUserId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Races { get; set; }
    }
}
