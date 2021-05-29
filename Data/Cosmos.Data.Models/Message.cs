namespace Cosmos.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Cosmos.Data.Common.Models;

    public class Message : BaseDeletableModel<string>
    {
        public Message()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [StringLength(25)]
        public string Title { get; set; }

        public string Content { get; set; }

        [Required]
        public string SenderId { get; set; }

        [NotMapped]
        public Player Sender { get; set; }

        [Required]
        public string ReceiverId { get; set; }

        [NotMapped]
        public Player Receiver { get; set; }

        public DateTime SentOn { get; set; }

        [Required]
        public bool IsRead { get; set; }
    }
}
