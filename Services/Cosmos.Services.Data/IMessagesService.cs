namespace Cosmos.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Cosmos.Web.ViewModels.Messages;

    public interface IMessagesService
    {
        Task SendMessage(MessageInputModel input, string senderId);

        Task DeleteMessage(string messageId);

        ICollection<MessageViewModel> ViewAllMessages(string id);

        MessageViewModel GetMessageById(string id);
    }
}
