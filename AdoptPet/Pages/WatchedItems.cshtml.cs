using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Entities.Models;
using AdoptPet.Pages.Ads;
using AdoptPet.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;


namespace AdoptPet.Pages
{
    public class WatchedItemsModel : DI_BasePageModel
    {
        private readonly ApplicationDbContext _context;

        public WatchedItemsModel(ApplicationDbContext context, IAuthorizationService authorizationService, UserManager<IdentityUser> userManager)
              : base(context, authorizationService, userManager)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["AdId"] = new SelectList(_context.Ad, "Id", "Description");
            return Page();
        }

        [BindProperty]
        public WatchedItem WatchedItem { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.WatchedItem.Add(WatchedItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
