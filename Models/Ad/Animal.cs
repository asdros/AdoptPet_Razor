using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdoptPet.Models
{
    public class Animal
    {
        [Column("AnimalId")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Typ zwierzęcia jest wymagany.")]
        public string Species { get; set; }

        public ICollection<Breed> Breeds { get; set; }

    }
}
