namespace Cosmos.Services.Data
{
    using System.Collections.Generic;

    using Cosmos.Web.ViewModels.Researches;

    public interface IResearchesService
    {
        ICollection<ResearchViewModel> GetAllResearches();

        ResearchViewModel GetResearchById(string researchId);

        void StartResearch(string researchId);
    }
}
