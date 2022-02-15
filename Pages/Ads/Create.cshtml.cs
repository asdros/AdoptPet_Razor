using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AdoptPet.Data;
using AdoptPet.Models;
using System.Web.Helpers;

namespace AdoptPet.Pages.Ads
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["AnimalId"] = new SelectList(_context.Animal, "Id", "Species");
        ViewData["BreedId"] = new SelectList(_context.Breed.Where(b=>b.AnimalId.Equals(), "Id", "Name");
        ViewData["PlaceId"] = new SelectList(_context.Place, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Ad Ad { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Ad.AvailableFrom = DateTime.Now;
            Ad.NormalizedLink = Ad.GenerateLink(Ad.Title);

            _context.Ad.Add(Ad);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
