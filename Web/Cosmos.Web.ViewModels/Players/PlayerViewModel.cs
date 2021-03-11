namespace Cosmos.Web.ViewModels.Players
{
    using AutoMapper;
    using Cosmos.Data.Models;
    using Cosmos.Services.Mapping;

    public class PlayerViewModel : IMapFrom<Player>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string PlayerName { get; set; }

        public string Alliance { get; set; }

        public string Moto { get; set; }

        public string Image { get; set; }

        public string ImageId { get; set; }

        public int Experience { get; set; }

        public string Rank { get; set; }

        public string Race { get; set; }

        public int Money { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Player, PlayerViewModel>()
                .ForMember(x => x.Rank, opt => opt.MapFrom(x => x.Rank.RankName))
                .ForMember(x => x.Race, opt => opt.MapFrom(x => x.Race.Name))
                .ForMember(x => x.Alliance, opt => opt.MapFrom(x => x.Alliance.Name))
                .ForMember(x => x.Image, opt => opt.Ignore());
        }
    }
}
