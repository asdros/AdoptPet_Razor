using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Province
    {
        [Column("ProvinceId")]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
