using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entities.Models
{
    public class Animal
    {
        [Column("AnimalId")]
        public int Id { get; set; }

        public string Species { get; set; }

        [JsonIgnore]
        public virtual ICollection<Breed> Breeds { get; set; }
    }
}
