namespace Cosmos.Web.ViewModels.Messages
{
    using AutoMapper;
    using Cosmos.Data.Models;
    using Cosmos.Services.Mapping;

    public class MessageViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string SenderName { get; set; }

        //public void CreateMappings(IProfileExpression configuration)
        //{
        //    configuration.CreateMap<Message, MessageViewModel>().ForMember(x => x.SenderName, opt => opt.MapFrom(x => x.Sender.PlayerName));
        //}
    }
}
