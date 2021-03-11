namespace Cosmos.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Cosmos.Data.Common.Repositories;
    using Cosmos.Data.Models;
    using Cosmos.Services.Mapping;
    using Cosmos.Web.ViewModels.Researches;

    public class ResearchesService : IResearchesService
    {
        private readonly IRepository<Research> researchRepository;

        public ResearchesService(IRepository<Research> researchRepository)
        {
            this.researchRepository = researchRepository;
        }

        public ICollection<ResearchViewModel> GetAllResearches()
        {
            return this.researchRepository.AllAsNoTracking().Select(x => x).To<ResearchViewModel>().ToList();
        }

        public ResearchViewModel GetResearchById(string researchId)
        {
            return this.researchRepository.AllAsNoTracking().Where(x => x.Id == researchId).To<ResearchViewModel>().FirstOrDefault();
        }

        public void StartResearch(string researchId)
        {
            var research = this.researchRepository.All().FirstOrDefault(x => x.Id == researchId);
            research.IsStarted = true;
            research.TimeStarted = DateTime.UtcNow;

        }
    }
}
