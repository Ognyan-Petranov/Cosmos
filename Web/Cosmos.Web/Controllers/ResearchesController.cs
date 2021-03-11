namespace Cosmos.Web.Controllers
{
    using Cosmos.Services.Data;
    using Cosmos.Web.Infrastructure.Filters;
    using Cosmos.Web.ViewModels.Researches;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    [TypeFilter(typeof(ValidatePlayerIsCreatedActionFilter))]
    public class ResearchesController : BaseController
    {
        private readonly IResearchesService researchesService;

        public ResearchesController(IResearchesService researchesService)
        {
            this.researchesService = researchesService;
        }

        [HttpGet]
        public IActionResult All()
        {
            var model = new ListAllResearchesViewModel
            {
                AllResearches = this.researchesService.GetAllResearches(),
            };

            return this.View(model);
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            var model = this.researchesService.GetResearchById(id);
            return this.View(model);
        }
    }
}
