namespace Cosmos.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Cosmos.Data.Common.Repositories;
    using Cosmos.Data.Models;

    public class RacesService : IRacesService
    {
        private readonly IRepository<Race> racesRepository;

        public RacesService(IRepository<Race> racesRepository)
        {
            this.racesRepository = racesRepository;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllRacesAsKeyValuePairs()
        {
            return this.racesRepository.AllAsNoTracking()
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                })
                .OrderBy(x => x.Name)
                .ToList().Select(x => new KeyValuePair<string, string>(x.Id, x.Name));
        }
    }
}
