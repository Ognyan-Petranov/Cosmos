namespace Cosmos.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Cosmos.Data.Common.Repositories;
    using Cosmos.Data.Models;
    using Cosmos.Services.Mapping;
    using Cosmos.Web.ViewModels.Messages;

    public class MessagesService : IMessagesService
    {
        private readonly IRepository<Message> messagesRepository;
        private readonly IRepository<Player> playersRepository;

        public MessagesService(IRepository<Message> messagesRepository, IRepository<Player> playersRepository)
        {
            this.messagesRepository = messagesRepository;
            this.playersRepository = playersRepository;
        }

        public async Task DeleteMessage(string id)
        {
            var message = this.messagesRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == id);
            this.messagesRepository.Delete(message);
            await this.messagesRepository.SaveChangesAsync();
        }

        public async Task SendMessage(MessageInputModel input, string senderId)
        {
            var message = new Message
            {
                SenderId = senderId,
                ReceiverId = this.playersRepository.AllAsNoTracking().FirstOrDefault(x => x.PlayerName == input.ReceiverName).Id,
                Title = input.Title,
                Content = input.Content,
                IsRead = false,
                SentOn = DateTime.UtcNow,
            };
            await this.messagesRepository.AddAsync(message);
            await this.messagesRepository.SaveChangesAsync();
        }

        public ICollection<MessageViewModel> ViewAllMessages(string id)
        {
            return this.messagesRepository.AllAsNoTracking().Where(x => x.ReceiverId == id).Select(x => new MessageViewModel
            {
                Id = x.Id,
                Content = x.Content,
                SenderName = x.Sender.PlayerName,
                Title = x.Title,
            }).ToList();
        }

        public MessageViewModel GetMessageById(string id)
        {
            return this.messagesRepository.AllAsNoTracking().Where(x => x.Id == id).To<MessageViewModel>().FirstOrDefault();
        }
    }
}
