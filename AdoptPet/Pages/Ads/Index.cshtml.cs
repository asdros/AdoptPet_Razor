using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AdoptPet.Data;
using Entities.Models;
using Contracts;

namespace AdoptPet.Pages.Ads
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IImageService _imageService;

        public IndexModel(ApplicationDbContext context, IImageService imageService)
        {
            _context = context;
            _imageService = imageService;
        }

        public IList<Ad> Ad { get; set; }

        [BindProperty]
        public Ad AdToDelete { get; set; }

        public async Task OnGetAsync()
        {
            Ad = await _context.Ad
                .Include(a => a.Breed)
                .Include(a => a.Place)
                .Include(x => x.Images.Where(i => i.isPoster.Equals(true)))
                .OrderBy(a => a.AvailableFrom)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(Guid? id)
        {
            var images = await _context.Image
                                    .Where(i => i.AdId.Equals(id))
                                     .ToListAsync();

            if (id == null)
            {
                return NotFound();
            }

            //AdToDelete = await _context.Ad.FindAsync(id);

            AdToDelete = await _context.Ad.Where(a =>
                                                    a.Id.Equals(id))
                                                    .Include(a => a.Images.Where(i => i.AdId.Equals(id)))
                                                    .SingleOrDefaultAsync();

            foreach (var image in images)
            {
                _imageService.DeleteImage(image);
                 _context.Image.Remove(image);
            }

            if (AdToDelete != null)
            {
                _context.Ad.Remove(AdToDelete);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
