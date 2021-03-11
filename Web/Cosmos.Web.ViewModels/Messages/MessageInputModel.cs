namespace Cosmos.Web.ViewModels.Messages
{
    using AutoMapper;
    using Cosmos.Data.Models;
    using Cosmos.Services.Mapping;

    public class MessageInputModel : IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string ReceiverId { get; set; }

        public string ReceiverName { get; set; }

        public string SenderId { get; set; }

        public string SenderName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Message, MessageInputModel>().ForMember(x => x.ReceiverName, opt => opt.MapFrom(x => x.Receiver.PlayerName))
                .ForMember(x => x.SenderName, opt => opt.MapFrom(x => x.Sender.PlayerName));
        }
    }
}
