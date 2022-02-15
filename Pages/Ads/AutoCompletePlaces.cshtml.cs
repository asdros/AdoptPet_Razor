using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AdoptPet.Data;
using AdoptPet.Models;
using AdoptPet.Models.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdoptPet.Pages.Ads
{
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
                .Where(p => p.Name.Contains(term))
                .ToList();

            var result = _mapper.Map<IEnumerable<PlaceDTO>>(placesList);

            return new JsonResult(result);
        }
    }
}
