using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdoptPet.Models
{
    public class Breed
    {
        [Column("BreedId")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Rasa zwierzęcia jest wymagana.")]
        public string Name { get; set; }

        [ForeignKey(nameof(Animal))]
        public int AnimalId { get; set; }
        public Animal Animal { get; set; }
    }
}
