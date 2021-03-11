namespace Cosmos.Web.ViewModels.Alliances
{
    using System.ComponentModel.DataAnnotations;
    using System.IO;

    using Cosmos.Data.Models;
    using Cosmos.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class CreateAllianceInputModel : IMapTo<Alliance>
    {
        [Required]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public string AllianceLeaderId { get; set; }

        public IFormFile AllianceImage { get; set; }
    }
}
