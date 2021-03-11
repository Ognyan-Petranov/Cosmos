namespace Cosmos.Web.Infrastructure.Filters
{
    using System.Linq;

    using Cosmos.Data.Common.Repositories;
    using Cosmos.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.Routing;

    public class ValidatePlayerIsCreatedActionFilter : IActionFilter
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IRepository<Player> playersRepository;

        public ValidatePlayerIsCreatedActionFilter(UserManager<ApplicationUser> userManager, IRepository<Player> playersRepository)
        {
            this.userManager = userManager;
            this.playersRepository = playersRepository;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var userId = this.userManager.GetUserId(context.HttpContext.User);
                if (!this.playersRepository.AllAsNoTracking().Any(x => x.ApplicationUserId == userId))
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { Controller = "Players", Action = "Create" }));
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
