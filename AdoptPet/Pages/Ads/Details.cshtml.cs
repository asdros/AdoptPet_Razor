using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AdoptPet.Data;
using Entities.Models;
using System.Collections.Generic;
using System.Linq;
using AdoptPet.Areas.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System;

namespace AdoptPet.Pages.Ads
{
    [AllowAnonymous]
    public class DetailsModel : DI_BasePageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context, IAuthorizationService authorizationService, UserManager<IdentityUser> userManager)
            :base(context,authorizationService,userManager)
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

            var isAuthorized = User.IsInRole(Constants.ManagersRole) || User.IsInRole(Constants.AdministratorsRole);

            var currentUserId = UserManager.GetUserId(User);

            if(!isAuthorized && currentUserId!=Ad.OwnerId && Ad.Status!=AdStatus.Zatwierdzone)
            {
                return Forbid();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id, AdStatus status)
        {
            var ad = await _context.Ad.FirstOrDefaultAsync(a => a.Id == id);

            if(ad==null)
            {
                return NotFound();
            }

            var adOperation = (status == AdStatus.Zatwierdzone) ? UserOperations.Approve : UserOperations.Reject;

            var isAuthorized = await AuthorizationService.AuthorizeAsync(User, ad, adOperation);
            
            if(!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            ad.Status = status;
            _context.Ad.Update(ad);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
