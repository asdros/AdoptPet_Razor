using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdoptPet.Data;
using AspNetCoreHero.ToastNotification.Abstractions;
using Entities.Models.Conversation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AdoptPet.Pages.Conversations
{
    public class IndexModel : DI_BasePageModel
    {
        private readonly ApplicationDbContext _context;
        
        private readonly INotyfService _notyfService;

        public IndexModel(ApplicationDbContext context, IAuthorizationService authorizationService, UserManager<IdentityUser> userManager, INotyfService notyfService) : base(context, authorizationService, userManager)
        {
            _context = context;
            _notyfService = notyfService;
        }

        [BindProperty]
        public IList<Chat> Chats { get; set; }
        public string currentUserId { get; set; }

        public Chat Chat { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            currentUserId = UserManager.GetUserId(User);

            if (currentUserId == null)
            {
                return Forbid();
            }

            //get a conversation about user's ads and the author of the messages
            Chats = await _context.Chat.Where(c => c.CreatedByUserId.Equals(currentUserId) || c.Ad.OwnerId.Equals(currentUserId))
                .Include(c => c.Ad.Images.Where(i => i.isPoster.Equals(true)))
                .ToListAsync();

            if (Chats.Any())
            {
                foreach (var chat in Chats)
                {
                    var user = await UserManager.FindByIdAsync(chat.CreatedByUserId);
                    chat.UsernameOfCreator = user.UserName.Remove(user.UserName.IndexOf("@"));

                    var AdOwnerUsername = await UserManager.FindByIdAsync(chat.Ad.OwnerId);
                    chat.AdOwnerUsername = AdOwnerUsername.UserName.Remove(AdOwnerUsername.UserName.IndexOf("@"));

                    var authorOfLastMsg = await UserManager.FindByIdAsync(chat.Messages.OrderByDescending(m => m.DateOfSending).Select(m => m.SendByUserId).FirstOrDefault());
                    chat.Messages.OrderByDescending(m=>m.DateOfSending).FirstOrDefault().UsernameOfSender = authorOfLastMsg.UserName.Remove(authorOfLastMsg.UserName.IndexOf("@"));
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            var chat = await _context.Chat.Where(c => c.Id.Equals(id)).SingleOrDefaultAsync();

            if(chat==null)
            {
                _notyfService.Warning("Wewnêtrzny b³¹d podczas usuwania.");
                return RedirectToPage("./Conversations/Index");
            }

            var currentUserId = UserManager.GetUserId(User);

            if (chat.CreatedByUserId != currentUserId)
            {
                _notyfService.Warning("Niedozwolona akcja."); // only the author of the conversation can delete 
                return RedirectToPage("/Conversations/Index");
            }

            _context.Chat.Remove(chat);
            await _context.SaveChangesAsync();

            _notyfService.Success("Konwersacja zosta³a usuniêta.");
            return RedirectToPage("/Conversations/Index");
        }

        public async Task<IActionResult> OnPostCreateNewChatAsync(Guid adId)
        {
            var ad = await _context.Ad.Where(a => a.Id.Equals(adId)).SingleOrDefaultAsync(); ;

            if(ad == null)
            {
                _notyfService.Warning("Og³oszenie jest nieosi¹galne.");
                return RedirectToPage("./IndexAnon");
            }

            Chat.CreatedByUserId = UserManager.GetUserId(User);
            Chat.AdId = ad.Id;

            _context.Chat.Add(Chat);

            await _context.SaveChangesAsync();

            return RedirectToPage("/Conversations/Messages", new { chatId = Chat.Id });
        }
    }
}
