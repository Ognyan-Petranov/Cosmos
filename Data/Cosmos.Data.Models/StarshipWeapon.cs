namespace Cosmos.Data.Models
{
    public class StarshipWeapon
    {
        public string StarshipId { get; set; }

        public Starship Starship { get; set; }

        public string WeaponId { get; set; }

        public Weapon Weapon { get; set; }
    }
}
