using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using AdoptPet.Pages.Ads;
using AdoptPet.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace AdoptPet.Pages
{
    public class WatchedItemsModel : DI_BasePageModel
    {
        private readonly ApplicationDbContext _context;

        private readonly INotyfService _notyfService;

        public WatchedItemsModel(ApplicationDbContext context, IAuthorizationService authorizationService, UserManager<IdentityUser> userManager, INotyfService notyfService)
              : base(context, authorizationService, userManager)
        {
            _context = context;
            _notyfService = notyfService;
        }

        [BindProperty]
        public IList<WatchedItem> WatchedItems { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {

            var currentUserId = UserManager.GetUserId(User);

            if(currentUserId==null)
            {
                return Forbid();
            }

            WatchedItems = await _context.WatchedItem
                .Include("Ad.Images")
                .Where(w => w.OwnerId.Equals(currentUserId))
                .Where(w => w.Ad.Images.Any(i => i.isPoster.Equals(true)))
                .ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {

            var watchItem = await _context.WatchedItem.Where(w => w.Id.Equals(id)).SingleOrDefaultAsync();

            _context.Remove(watchItem);
            await _context.SaveChangesAsync();

            _notyfService.Success("Ogłoszenie usunięto z listy obserwowanych");
            return RedirectToPage("./WatchedItems");
        }
    }
}
