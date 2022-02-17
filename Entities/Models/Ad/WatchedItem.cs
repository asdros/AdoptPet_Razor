using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class WatchedItem
    {
        [Column("WatchedItemId")]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Ad))]
        public Guid AdId { get; set; }
        public virtual Ad Ad { get; set; }

        public string OwnerId { get; set; }
    }
}
