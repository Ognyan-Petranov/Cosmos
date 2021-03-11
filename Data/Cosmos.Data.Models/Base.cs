namespace Cosmos.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Cosmos.Data.Common.Models;

    public class Base : BaseDeletableModel<string>
    {
        public Base()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Players = new HashSet<Player>();
        }

        public string Name { get; set; }

        public int Level { get; set; }

        public int Shields { get; set; }

        public int Structure { get; set; }

        public int Turrets { get; set; }

        public ICollection<Player> Players { get; set; }

        public string WarehouseId { get; set; }

        public Warehouse Warehouse { get; set; }
    }
}
