namespace Cosmos.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Cosmos.Data.Models;

    public class RanksSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Ranks.Any())
            {
                return;
            }

            await dbContext.Ranks.AddAsync(new Rank { RankName = "Civilian" });
            await dbContext.Ranks.AddAsync(new Rank { RankName = "Private" });
            await dbContext.Ranks.AddAsync(new Rank { RankName = "Corporal" });
            await dbContext.Ranks.AddAsync(new Rank { RankName = "Sergeant" });
            await dbContext.Ranks.AddAsync(new Rank { RankName = "Staff Sergeant" });
            await dbContext.Ranks.AddAsync(new Rank { RankName = "Sergeant Major" });
            await dbContext.Ranks.AddAsync(new Rank { RankName = "First Lieutenant" });
            await dbContext.Ranks.AddAsync(new Rank { RankName = "Captain" });
            await dbContext.Ranks.AddAsync(new Rank { RankName = "Major" });
            await dbContext.Ranks.AddAsync(new Rank { RankName = "Colonel" });
            await dbContext.Ranks.AddAsync(new Rank { RankName = "General" });
            await dbContext.Ranks.AddAsync(new Rank { RankName = "Fleet Commander" });

            await dbContext.SaveChangesAsync();
        }
    }
}
