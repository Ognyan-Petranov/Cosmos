namespace Cosmos.Web.Controllers
{
    using System;
    using System.IO;

    using Cosmos.Web.Infrastructure.Filters;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Net.Http.Headers;

    [Authorize]
    [TypeFilter(typeof(ValidatePlayerIsCreatedActionFilter))]
    public class ImagesController : BaseController
    {
        private IActionResult ReturnImage(Stream image)
        {
            var headers = this.Response.GetTypedHeaders();

            headers.CacheControl = new CacheControlHeaderValue
            {
                Public = true,
                MaxAge = TimeSpan.FromDays(30),
            };

            headers.Expires = new DateTimeOffset(DateTime.UtcNow.AddDays(30));

            return this.File(image, "image/jpeg");
        }
    }
}
