using AutoMapper;
using Entities.Models;
using Entities.DTO;

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

            CreateMap<ImageForCreationDTO, Image>();
        }
    }
}