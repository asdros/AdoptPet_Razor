using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdoptPet.Models
{
    public class Image
    {
        [Column("ImageId")]
        public Guid Id { get; set; }

        public bool isPoster { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        public Guid AdId { get; set; }
        public virtual Ad Ad { get; set; }
    }
}
