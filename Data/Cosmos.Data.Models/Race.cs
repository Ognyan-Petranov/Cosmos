namespace Cosmos.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Cosmos.Data.Common.Models;

    public class Race : BaseModel<string>
    {
        public Race()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Players = new HashSet<Player>();
            this.Starships = new HashSet<Starship>();
        }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Player> Players { get; set; }

        public virtual ICollection<Starship> Starships { get; set; }
    }
}
