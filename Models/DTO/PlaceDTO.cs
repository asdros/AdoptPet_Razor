using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdoptPet.Models.DTO
{
    public class PlaceDTO
    {
        public int PlaceId { get; set; }
        public string FullPlaceName { get; set; } // place, district, province
    }
}
