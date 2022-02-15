using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AdoptPet.Data;
using AdoptPet.Models;

namespace AdoptPet.Pages.Ads
{
    public class DeleteModel : PageModel
    {
        private readonly AdoptPet.Data.ApplicationDbContext _context;

        public DeleteModel(AdoptPet.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Ad Ad { get; set; }

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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Ad = await _context.Ad.FindAsync(id);

            if (Ad != null)
            {
                _context.Ad.Remove(Ad);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
