﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdoptPet.Models
{
    public class WatchedItem
    {
        [Column("WatchedItemId")]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Ad))]
        public Guid AdId { get; set; }
        public Ad Ad { get; set; }

        public string OwnerId { get; set; }
    }
}