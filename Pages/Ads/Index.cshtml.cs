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
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Ad> Ad { get;set; }

        public async Task OnGetAsync()
        {
            Ad = await _context.Ad
                .Include(a => a.Breed)
                .Include(a => a.Place)
                .Include(x=>x.Images.Where(i=>i.isPoster.Equals(true)))
                .OrderBy(a => a.AvailableFrom)
                .ToListAsync();
        }
    }
}
