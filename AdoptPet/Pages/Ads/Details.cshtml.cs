using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AdoptPet.Data;
using Entities.Models;
using System.Collections.Generic;
using System.Linq;

namespace AdoptPet.Pages.Ads
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Ad Ad { get; set; }

        public IEnumerable<Image> Images { get; set; }

        public async Task<IActionResult> OnGetAsync(string link)
        {
            if (link == null)
            {
                return NotFound();
            }

            Ad = await _context.Ad
                .Include(a => a.Breed)
                .Include(a=>a.Breed.Animal)
                .Include(a => a.Place)
                .Include(a=>a.Place.District)
                .Include(a => a.Place.District.Province)
                .FirstOrDefaultAsync(m => m.NormalizedLink == link);

            Images = await _context.Image
                .Where(i => 
                        i.AdId.Equals(Ad.Id))
                        .ToListAsync();

            if (Ad == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
