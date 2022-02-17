using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entities.Models
{
    public class Breed
    {
        [Column("BreedId")]
        public int Id { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(Animal))]
        public int AnimalId { get; set; }
        public virtual Animal Animal { get; set; }
    }
}
