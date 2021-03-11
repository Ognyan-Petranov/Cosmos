namespace Cosmos.Web.ViewModels.Alliances
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using Cosmos.Data.Models;
    using Cosmos.Services.Mapping;
    using Cosmos.Web.ViewModels.Players;
    using Microsoft.AspNetCore.Http;

    public class AllianceViewModel : IMapFrom<Alliance>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string AllianceLeader { get; set; }

        public ICollection<PlayerViewModel> Players { get; set; }

        public string RenderImage { get; set; }

        [Display(Name = "Upload New Image")]
        public IFormFile NewAllianceImage { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
        }
    }
}
