using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdoptPet.Data;
using Entities.Models;

namespace AdoptPet.Pages.Ads
{
    public class EditModel : PageModel
    {
        private readonly AdoptPet.Data.ApplicationDbContext _context;

        public EditModel(AdoptPet.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Ad Ad { get; set; }
        
        [BindProperty]
        public List<Image> Images { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Ad = await _context.Ad
                .Include(a => a.Breed)
                .Include(a => a.Place).FirstOrDefaultAsync(m => m.Id == id);

            if (Ad == null)
            {
                return NotFound();
            }
           ViewData["BreedId"] = new SelectList(_context.Breed, "Id", "Name");
           ViewData["PlaceId"] = new SelectList(_context.Place, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Ad).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdExists(Ad.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AdExists(Guid id)
        {
            return _context.Ad.Any(e => e.Id == id);
        }
    }
}
