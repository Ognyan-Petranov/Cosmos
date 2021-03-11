namespace Cosmos.Web.ViewModels.Researches
{
    using System;

    using Cosmos.Data.Models;
    using Cosmos.Services.Mapping;

    public class ResearchViewModel : IMapFrom<Research>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public TimeSpan ResearchTime { get; set; }

        public int ExperienceGiven { get; set; }
    }
}
