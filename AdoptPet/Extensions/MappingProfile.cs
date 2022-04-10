using AutoMapper;
using Entities.Models;
using Entities.DTO;
using Microsoft.AspNetCore.Identity;

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
            CreateMap<AdForUpdateDTO, Ad>().ReverseMap();
            CreateMap<IdentityUser, UserDTO>();

            CreateMap<Breed, BreedManagerViewDTO>()
                .ForMember(d =>d.AnimalSpecies, opt =>opt.MapFrom(s=>s.Animal.Species));
        }
    }
}