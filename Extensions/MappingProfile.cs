using AdoptPet.Models;
using AdoptPet.Models.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdoptPet.Extensions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Place, PlaceDTO>()
                .ForMember(p => p.PlaceId, opt => 
                    opt.MapFrom(s => s.Id))
                .ForMember(p=>p.FullPlaceName, opt=> 
                    opt.MapFrom(s=> string
                        .Join(", ", s.Name, s.District.Name, s.District.Province.Name)));
        }
    }
}