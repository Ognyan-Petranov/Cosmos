namespace Cosmos.Data.Models
{
    public class PlayerStarship
    {
        public string PlayerId { get; set; }

        public Player Player { get; set; }

        public string StarshipId { get; set; }

        public Starship Starship { get; set; }
    }
}
