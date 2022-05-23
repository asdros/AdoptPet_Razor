using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models.Conversation
{
    public class Message
    {
        [Column("MessageId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage ="Treść wiadomości nie może być pusta.")]
        [MaxLength(500, ErrorMessage = "Maksymalna długość to 500 znaków.")]
        public string TextMessage { get; set; }

        public string SendByUserId { get; set; }
        [NotMapped]
        public string UsernameOfSender { get; set; }

        [ForeignKey(nameof(Chat))]
        public Guid ChatId { get; set; }
        public virtual Chat Chat { get; set; }

        //validation to set a datatime during creating new entity to database
        private DateTime _dateOfSending;

        public DateTime DateOfSending
        {
            get { return _dateOfSending; }
            set
            {
                if (DateOfSending == DateTime.MinValue)
                {
                    value = DateTime.Now;
                }
                _dateOfSending = value;
            }
        }

    }
}
