namespace Cosmos.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;

    using Cosmos.Data.Common.Models;

    public class Alliance : BaseDeletableModel<string>
    {
        public Alliance()
        {
            this.Id = Guid.NewGuid().ToString();
            this.MembersCount = 50;
            this.Players = new HashSet<Player>();
        }

        [Required]
        public string Name { get; set; }

        public int MembersCount { get; set; }

        public string AllianceLeader { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public string ImageId { get; set; }

        public CosmosImage Image { get; set; }

        public string OriginalImageId { get; set; }

        public CosmosOriginalImage OriginalImage { get; set; }

        public virtual ICollection<Player> Players { get; set; }
    }
}
