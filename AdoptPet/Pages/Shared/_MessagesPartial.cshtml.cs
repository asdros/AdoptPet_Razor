using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AdoptPet.Data;
using Entities.Models.Conversation;

namespace AdoptPet.Pages.Shared
{
    public class _MessagesPartialModel : PageModel
    {
        private readonly AdoptPet.Data.ApplicationDbContext _context;

        public _MessagesPartialModel()
        {

        }

        public _MessagesPartialModel(AdoptPet.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ChatId"] = new SelectList(_context.Chat, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Message Message { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Message.Add(Message);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
