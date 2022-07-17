using AdoptPet.Data;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdoptPet.Pages
{
    [AllowAnonymous]
    public class IndexAnonModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly ApplicationDbContext _context;

        public IList<Ad> ApprovedAds { get; set; }

        public Ad AdToSearch { get; set; }

        public IndexAnonModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task OnGetAsync(string titleFilter, bool? sterilizationFilter, int? animalFilter, int? breedFilter, int? provinceFilter, string districtFilter, string placeFilter, Gender? genderAnimalFilter)
        {
            //data for dropdown lists
            ViewData["AnimalId"] = new SelectList(_context.Animal, "Id", "Species");
            ViewData["GenderAnimal"] = new SelectList(Enum.GetValues(typeof(Gender)));
            ViewData["ProvinceId"] = new SelectList(_context.Province, "Id", "Name");

            ApprovedAds = await _context.Ad
              .Where(a => a.Status.Equals(AdStatus.Zatwierdzone))
              .Include(a => a.Breed)
              .Include(a => a.Place)
              .Include(a => a.Images.Where(i => i.isPoster.Equals(true)))
              .OrderBy(a => a.AvailableFrom)
              .Take(9)
              .ToListAsync();

            //extract the name of the locality from the full location data
            if(placeFilter!= null && placeFilter.Contains(','))
            {
                placeFilter = placeFilter.Substring(0, placeFilter.IndexOf(","));
            }

            //filtering results
            ApprovedAds = ApprovedAds
                .Where(a => (titleFilter != null && a.Title.ToLower().StartsWith(titleFilter.ToLower())) || (titleFilter == null))
                .Where(a => (sterilizationFilter != null && a.Sterilization == sterilizationFilter) || (sterilizationFilter == null))
                .Where(a => (animalFilter != null && a.Breed.Animal.Id == animalFilter) || (animalFilter == null))
                .Where(a => (breedFilter != null && a.Breed.Id == breedFilter) || (breedFilter == null))
                .Where(a => (provinceFilter != null && a.Place.District.Province.Id == provinceFilter) || (provinceFilter == null))
                .Where(a => (placeFilter != null && a.Place.Name.ToLower().StartsWith(placeFilter.ToLower())) || (placeFilter == null))
                .Where(a => (districtFilter != null && a.Place.District.Name == districtFilter) || (districtFilter == null))
                .Where(a => (genderAnimalFilter != null && a.GenderAnimal == genderAnimalFilter) || (genderAnimalFilter == null))
                .ToList();

        }
    }
}
