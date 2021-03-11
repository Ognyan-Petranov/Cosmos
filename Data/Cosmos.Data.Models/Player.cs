namespace Cosmos.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Cosmos.Data.Common.Models;

    public class Player : BaseDeletableModel<string>
    {
        public Player()
        {
            this.Id = Guid.NewGuid().ToString();
            this.PlayerStarships = new HashSet<PlayerStarship>();
            this.Messages = new HashSet<Message>();
            this.Bases = new HashSet<Base>();
        }

        [Required]
        [StringLength(30)]
        public string PlayerName { get; set; }

        public int Experience { get; set; }

        [Required]
        public string RankId { get; set; }

        public Rank Rank { get; set; }

        [Required]
        public string RaceId { get; set; }

        public Race Race { get; set; }

        public string ImageId { get; set; }

        public CosmosImage Image { get; set; }

        public string OriginalImageId { get; set; }

        public CosmosOriginalImage OriginalImage { get; set; }

        public int Money { get; set; }

        public string AllianceId { get; set; }

        public Alliance Alliance { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<PlayerStarship> PlayerStarships { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        public ICollection<Base> Bases { get; set; }
    }
}
