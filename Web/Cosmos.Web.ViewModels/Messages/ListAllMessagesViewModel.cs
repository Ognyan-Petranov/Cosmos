namespace Cosmos.Web.ViewModels.Messages
{
    using System.Collections.Generic;

    public class ListAllMessagesViewModel
    {
        public ICollection<MessageViewModel> AllMessages { get; set; }
    }
}
