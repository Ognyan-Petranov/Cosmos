namespace Cosmos.Web.Controllers
{
    using Cosmos.Web.Infrastructure.Filters;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class LiveChatsController : Controller
    {
        [Authorize]
        [TypeFilter(typeof(ValidatePlayerIsCreatedActionFilter))]
        public IActionResult LiveChat()
        {
            return this.View();
        }
    }
}
