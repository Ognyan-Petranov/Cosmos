namespace Cosmos.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Cosmos.Data.Common.Models;

    public class Weapon : BaseModel<string>
    {
        public Weapon()
        {
            this.Id = Guid.NewGuid().ToString();
            this.StarshipWeapons = new HashSet<StarshipWeapon>();
        }

        [Required]
        public string Name { get; set; }

        public int Damage { get; set; }

        public virtual ICollection<StarshipWeapon> StarshipWeapons { get; set; }
    }
}
