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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using AdoptPet.Areas.Authorization;

namespace AdoptPet.Pages.Ads
{
    public class IndexModel : DI_BasePageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IImageService _imageService;

        public IndexModel(ApplicationDbContext context, IAuthorizationService authorizationService, UserManager<IdentityUser> userManager, IImageService imageService)
            : base(context, authorizationService, userManager)
        {
            _context = context;
            _imageService = imageService;
        }

        public IList<Ad> Ad { get; set; }

        public IList<Ad> ApprovedAds { get; set; }

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

            ApprovedAds = await _context.Ad
                .Where(a=>a.Status.Equals(AdStatus.Zatwierdzone))
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


            AdToDelete = await _context.Ad.Where(a =>
                                                    a.Id.Equals(id))
                                                    .Include(a => a.Images.Where(i => i.AdId.Equals(id)))
                                                    .SingleOrDefaultAsync();

            if (AdToDelete == null)
            {
                return NotFound();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(User, AdToDelete, UserOperations.Delete);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            foreach (var image in images)
            {
                _imageService.DeleteImage(image);
                _context.Image.Remove(image);
            }


            _context.Ad.Remove(AdToDelete);
            await _context.SaveChangesAsync();


            return RedirectToPage("./Index");
        }
    }
}
