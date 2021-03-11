﻿namespace Cosmos.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Cosmos.Data.Common.Models;

    public class CosmosImage : BaseDeletableModel<string>
    {
        public CosmosImage()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Players = new HashSet<Player>();
            this.Alliances = new HashSet<Alliance>();
        }

        public byte[] Content { get; set; }

        public ICollection<Player> Players { get; set; }

        public ICollection<Alliance> Alliances { get; set; }
    }
}
