﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Place
    {
        [Column("PlaceId")]
        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey(nameof(District))]
        public int DistrictId { get; set; }
        public virtual District District { get; set; }
    }
}
