namespace Cosmos.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Cosmos.Data.Models;

    public class ResearchesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Researches.Any())
            {
                return;
            }

            for (int i = 0; i < 50; i++)
            {
                await dbContext.Researches.AddAsync(new Research
                {
                    Name = $"Research{i + 1}",
                    Cost = 5_000,
                    ResearchTime = TimeSpan.FromSeconds(30),
                    ExperienceGiven = 10_000,
                });
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
