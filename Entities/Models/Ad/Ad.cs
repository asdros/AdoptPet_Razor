using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    public class Ad
    {
        [Column("AdId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Tytuł jest wymagany.")]
        [MaxLength(50, ErrorMessage = "Maksymalna długość to 50 znaków.")]
        [MinLength(10, ErrorMessage = "Minimalna długość to 10 znaków.")]
        public string Title { get; set; }

        private string _normalizedLink;

        // userfriendly url link for ad
        public string NormalizedLink
        {
            get { return _normalizedLink; }
            set
            {
                if (NormalizedLink == null || NormalizedLink == "")
                {
                    value = GenerateLink(Title);
                }
                _normalizedLink = value;
            }
        }

        [Required(ErrorMessage = "Opis jest wymagany.")]
        [MaxLength(1000, ErrorMessage = "Maksymalna długość to 1000 znaków.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Informacja na temat sterylizacji jest wymagana.")]
        public bool Sterilization { get; set; }

        [Required(ErrorMessage = "Informacja na temat odrobaczenia jest wymagana.")]
        public bool Deworming { get; set; }

        //validation to set a datatime during creating new entity to database
        private DateTime _availableFrom;

        public DateTime AvailableFrom
        {
            get { return _availableFrom; }
            set
            {
                if (AvailableFrom == DateTime.MinValue)
                {
                    value = DateTime.Now;
                }
                _availableFrom = value;
            }
        }

        public DateTime? LastModified { get; set; }

        public AdStatus Status { get; set; }

        public string OwnerId { get; set; }

        [Required(ErrorMessage = "Wiek zwierzęcia jest wymagany")]
        [Range(0, 1200, ErrorMessage = "Maksymalny wiek zwierzęcia to 100 lat (1200 miesięcy).")]
        public int AgeAnimal { get; set; }

        [Required(ErrorMessage = "Płeć zwierzęcia jest wymagana.")]
        public Gender GenderAnimal { get; set; }

        [Required(ErrorMessage = "Rasa zwierzęcia jest wymagana.")]
        [ForeignKey(nameof(Breed))]
        public int BreedId { get; set; }
        public virtual Breed Breed { get; set; }

        [Required(ErrorMessage = "Miejsce jest wymagane.")]
        [ForeignKey(nameof(Place))]
        public int PlaceId { get; set; }
        public virtual Place Place { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        //generate unique link to ad
        public static string GenerateLink(string title)
        {
            string encoded = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            string titleForLink = ConvertPolishChars(title);

            encoded = encoded
                .Replace("/", "_")
                .Replace("+", "-")
                .Substring(0, 5);

            return titleForLink.Substring(0, 5).Replace(" ", "-") + "-" + encoded;
        }

        public static string ConvertPolishChars(string text)
        {
            string resultText = text.ToLower();
            resultText = resultText.Replace('ą', 'a').Replace('ć', 'c').Replace('ę', 'e').Replace('ł', 'l')
                                   .Replace('ń', 'n').Replace('ó', 'o').Replace('ś', 's').Replace('ź', 'z').Replace('ż', 'z');

            return resultText;
        }
    }

    public enum AdStatus
    {
        Oczekujące,
        Zatwierdzone,
        Odrzucone
    }

    public enum Gender
    {
        Samiec,
        Samica
    }
}
