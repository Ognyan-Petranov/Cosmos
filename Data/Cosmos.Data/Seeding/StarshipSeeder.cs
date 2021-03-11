namespace Cosmos.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Cosmos.Data.Models;

    public class StarshipSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Starships.Any())
            {
                return;
            }

            await dbContext.Starships.AddAsync(new Starship { Name = "Assassin", Armor = 1000, Shields = 1000, IsShared = true, RaceId = "2a79952c-a61f-4749-9f27-8a14a78b453c", HeavyWeaponSlots = 3, MediumWeaponSlots = 3, LightWeaponSlots = 4, Cost = 125_250_000 });
            await dbContext.Starships.AddAsync(new Starship { Name = "Saboteur", Armor = 1200, Shields = 600, IsShared = true, RaceId = "2a79952c-a61f-4749-9f27-8a14a78b453c", HeavyWeaponSlots = 1, MediumWeaponSlots = 2, LightWeaponSlots = 4, Cost = 100_150_000 });
            await dbContext.Starships.AddAsync(new Starship { Name = "Dark Aura", Armor = 900, Shields = 700, IsShared = true, RaceId = "2a79952c-a61f-4749-9f27-8a14a78b453c", HeavyWeaponSlots = 1, MediumWeaponSlots = 2, LightWeaponSlots = 3, Cost = 85_150_000 });
            await dbContext.Starships.AddAsync(new Starship { Name = "Dreadnought", Armor = 1500, Shields = 500, IsShared = true, RaceId = "39ac947b-e24b-41fa-93b9-eb54432a2bac", HeavyWeaponSlots = 2, MediumWeaponSlots = 2, LightWeaponSlots = 4, Cost = 150_150_000 });
            await dbContext.Starships.AddAsync(new Starship { Name = "Battleship", Armor = 1200, Shields = 1000, IsShared = true, RaceId = "39ac947b-e24b-41fa-93b9-eb54432a2bac", HeavyWeaponSlots = 2, MediumWeaponSlots = 4, LightWeaponSlots = 4, Cost = 130_150_000 });
            await dbContext.Starships.AddAsync(new Starship { Name = "Light Cruiser", Armor = 1000, Shields = 1000, IsShared = true, RaceId = "39ac947b-e24b-41fa-93b9-eb54432a2bac", HeavyWeaponSlots = 2, MediumWeaponSlots = 3, LightWeaponSlots = 1, Cost = 90_150_000 });
            await dbContext.Starships.AddAsync(new Starship { Name = "Black Widow", Armor = 800, Shields = 900, IsShared = true, RaceId = "b0dc26ca-13f6-4eae-ab2d-ea62b827ae46", HeavyWeaponSlots = 2, MediumWeaponSlots = 4, LightWeaponSlots = 5, Cost = 150_150_000 });
            await dbContext.Starships.AddAsync(new Starship { Name = "Hoard Leader", Armor = 1500, Shields = 900, IsShared = true, RaceId = "b0dc26ca-13f6-4eae-ab2d-ea62b827ae46", HeavyWeaponSlots = 2, MediumWeaponSlots = 4, LightWeaponSlots = 1, Cost = 130_150_000 });
            await dbContext.Starships.AddAsync(new Starship { Name = "Hopper", Armor = 900, Shields = 600, IsShared = true, RaceId = "b0dc26ca-13f6-4eae-ab2d-ea62b827ae46", HeavyWeaponSlots = 1, MediumWeaponSlots = 2, LightWeaponSlots = 4, Cost = 90_150_000 });
            await dbContext.Starships.AddAsync(new Starship { Name = "Prime", Armor = 1200, Shields = 1100, IsShared = true, RaceId = "938d5e69-5d04-41f1-9935-6a07bcf365b2", HeavyWeaponSlots = 2, MediumWeaponSlots = 2, LightWeaponSlots = 4, Cost = 90_150_000 });
            await dbContext.Starships.AddAsync(new Starship { Name = "Destroyer", Armor = 900, Shields = 800, IsShared = true, RaceId = "938d5e69-5d04-41f1-9935-6a07bcf365b2", HeavyWeaponSlots = 1, MediumWeaponSlots = 4, LightWeaponSlots = 4, Cost = 90_150_000 });
            await dbContext.Starships.AddAsync(new Starship { Name = "Raptor-SX9", Armor = 800, Shields = 600, IsShared = true, RaceId = "938d5e69-5d04-41f1-9935-6a07bcf365b2", HeavyWeaponSlots = 0, MediumWeaponSlots = 3, LightWeaponSlots = 4, Cost = 90_150_000 });

            await dbContext.SaveChangesAsync();
        }
    }
}
