namespace Cosmos.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Cosmos.Data.Common.Models;

    public class Starship : BaseModel<string>
    {
        public Starship()
        {
            this.Id = Guid.NewGuid().ToString();
            this.StarshipWeapons = new HashSet<StarshipWeapon>();
            this.PlayerStarships = new HashSet<PlayerStarship>();
        }

        public string Name { get; set; }

        public int Shields { get; set; }

        public int Armor { get; set; }

        public int LightWeaponSlots { get; set; }

        public int MediumWeaponSlots { get; set; }

        public int HeavyWeaponSlots { get; set; }

        public string RaceId { get; set; }

        public Race Race { get; set; }

        public int Cost { get; set; }

        // Shows if a ship is for personal use only, or shared to all alliance members.
        [Required]
        public bool IsShared { get; set; }

        public byte[] StarshipImage { get; set; }

        public virtual ICollection<StarshipWeapon> StarshipWeapons { get; set; }

        public virtual ICollection<PlayerStarship> PlayerStarships { get; set; }
    }
}
