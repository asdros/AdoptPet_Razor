using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models.Conversation
{
    public class Message
    {
        [Column("MessageId")]
        public Guid Id { get; set; }

        [MaxLength(500, ErrorMessage = "Maksymalna długość to 1000 znaków.")]
        public string TextMessage { get; set; }

        public string OwnerId { get; set; }

        public string ReceiverId { get; set; }

        [ForeignKey(nameof(Chat))]
        public Guid ChatId { get; set; }
        public virtual Chat Chat { get; set; }

        //validation to set a datatime during creating new entity to database
        private DateTime _availableFrom;

        public DateTime AvailableFrom
        {
            get { return _availableFrom; }
            set
            {
                if (AvailableFrom == DateTime.MinValue)
                {
                    value = DateTime.Now;
                }
                _availableFrom = value;
            }
        }

    }
}
