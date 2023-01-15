using System.Collections.Generic;
using System.Linq;
using AdoptPet.Data;
using AutoMapper;
using Entities.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdoptPet.Pages.Ads
{
    [AllowAnonymous]
    public class AutoCompletePlacesModel : PageModel
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public AutoCompletePlacesModel(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public JsonResult OnGet(string term)
        {
            var placesList = _context.Place
                .Where(p => p.Name.StartsWith(term))
                .ToList();

            var result = _mapper.Map<IEnumerable<PlaceDTO>>(placesList);

            return new JsonResult(result);
        }
    }
}
