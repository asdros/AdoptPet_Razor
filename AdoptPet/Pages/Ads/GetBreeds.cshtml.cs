using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdoptPet.Data;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace AdoptPet.Pages.Ads
{
    public class GetBreedsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public GetBreedsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public JsonResult OnGet(int animalIdVal)
        {
            List<Breed> breeds = _context.Breed.Where(b => b.AnimalId.Equals(animalIdVal))
                        .ToList();

            return new JsonResult(breeds);
        }
    }
}

