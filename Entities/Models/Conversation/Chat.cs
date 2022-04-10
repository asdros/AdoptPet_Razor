using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models.Conversation
{
    public class Chat
    {
        [Column("ChatId")]
        public Guid Id { get; set; }

        public ChatStatus Status { get; set; }

        [ForeignKey(nameof(Ad))]
        public Guid AdId { get; set; }
        public virtual Ad Ad { get; set; }
    }

    public enum ChatStatus
    {
        Odczytane,
        Nieodczytane
    }
}
