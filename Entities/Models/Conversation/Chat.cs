﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models.Conversation
{
    public class Chat
    {
        [Column("ChatId")]
        public Guid Id { get; set; }

        public string CreatedByUserId { get; set; }
        [NotMapped]
        public string UsernameOfCreator { get; set; }

        [NotMapped]
        public string AdOwnerUsername { get; set; }

        [ForeignKey(nameof(Ad))]
        public Guid AdId { get; set; }
        public virtual Ad Ad { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}
