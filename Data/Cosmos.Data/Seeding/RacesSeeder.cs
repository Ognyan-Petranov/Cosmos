namespace Cosmos.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Cosmos.Data.Models;

    public class RacesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Races.Any())
            {
                return;
            }

            await dbContext.Races.AddAsync(new Race { Name = "Xollian" });
            await dbContext.Races.AddAsync(new Race { Name = "Mawlor" });
            await dbContext.Races.AddAsync(new Race { Name = "Paragon" });
            await dbContext.Races.AddAsync(new Race { Name = "Zyck'lirg" });

            await dbContext.SaveChangesAsync();
        }
    }
}
