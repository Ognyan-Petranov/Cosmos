namespace Cosmos.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Cosmos.Data.Common.Models;

    public class Rank : BaseModel<string>
    {
        public Rank()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Players = new HashSet<Player>();
        }

        [Required]
        public string RankName { get; set; }

        public ICollection<Player> Players { get; set; }
    }
}
