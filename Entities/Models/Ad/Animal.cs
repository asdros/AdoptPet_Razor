using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Animal
    {
        [Column("AnimalId")]
        public int Id { get; set; }

        public string Species { get; set; }

        public virtual ICollection<Breed> Breeds { get; set; }
    }
}
