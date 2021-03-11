namespace Cosmos.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Cosmos.Data.Common.Models;

    public class Warehouse : BaseDeletableModel<string>
    {
        public Warehouse()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Weapons = new HashSet<Weapon>();
            this.Resources = new HashSet<Resource>();
        }

        public int Capacity { get; set; }

        public ICollection<Weapon> Weapons { get; set; }

        public ICollection<Resource> Resources { get; set; }
    }
}
