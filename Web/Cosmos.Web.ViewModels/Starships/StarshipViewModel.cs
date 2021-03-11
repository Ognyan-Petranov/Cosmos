namespace Cosmos.Web.ViewModels.Starships
{
    using AutoMapper;
    using Cosmos.Data.Models;
    using Cosmos.Services.Mapping;

    public class StarshipViewModel : IMapFrom<Starship>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int Shields { get; set; }

        public string Armor { get; set; }

        public string Image { get; set; }

        public int LightWeaponSlots { get; set; }

        public int MediumWeaponSlots { get; set; }

        public int HeavyWeaponSlots { get; set; }

        public int Cost { get; set; }

        public string Race { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Starship, StarshipViewModel>()
                .ForMember(x => x.Race, opt => opt.MapFrom(x => x.Race.Name));
        }
    }
}
