using Entities.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTO
{
    public class AdForUpdateDTO
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Tytuł jest wymagany.")]
        [MaxLength(50, ErrorMessage = "Maksymalna długość to 50 znaków.")]
        [MinLength(10, ErrorMessage = "Minimalna długość to 10 znaków.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Opis jest wymagany.")]
        [MaxLength(1000, ErrorMessage = "Maksymalna długość to 1000 znaków.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Informacja na temat sterylizacji jest wymagana.")]
        public bool Sterilization { get; set; }

        [Required(ErrorMessage = "Informacja na temat odrobaczenia jest wymagana.")]
        public bool Deworming { get; set; }
        public DateTime LastModified { get; set; }

        [Required(ErrorMessage = "Wiek zwierzęcia jest wymagany")]
        [Range(0, 1200, ErrorMessage = "Maksymalny wiek zwierzęcia to 100 lat (1200 miesięcy).")]
        public int AgeAnimal { get; set; }

        [Required(ErrorMessage = "Płeć zwierzęcia jest wymagana.")]
        public Gender GenderAnimal { get; set; }

        [Required(ErrorMessage = "Rasa zwierzęcia jest wymagana.")]
        public int BreedId { get; set; }

        [Required(ErrorMessage = "Miejsce jest wymagane.")]
        public int PlaceId { get; set; }
        public string OwnerId { get; set; }
        public AdStatus Status { get; set; }
    }
}

