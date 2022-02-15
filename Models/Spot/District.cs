using System.ComponentModel.DataAnnotations.Schema;

namespace AdoptPet.Models
{
    public class District
    {
        [Column("DistrictId")]
        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey(nameof(Province))]
        public int ProvinceId { get; set; }
        public virtual Province Province { get; set; }
    }
}
