using AdoptPet.Data;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        public IndexAnonModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task OnGetAsync()
        {
            ApprovedAds = await _context.Ad
              .Where(a => a.Status.Equals(AdStatus.Zatwierdzone))
              .Include(a => a.Breed)
              .Include(a => a.Place)
              .Include(x => x.Images.Where(i => i.isPoster.Equals(true)))
              .OrderBy(a => a.AvailableFrom)
              .ToListAsync();
        }
    }
}
