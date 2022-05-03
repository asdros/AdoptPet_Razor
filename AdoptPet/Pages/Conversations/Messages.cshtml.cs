using AdoptPet.Data;
using AspNetCoreHero.ToastNotification.Abstractions;
using Entities.Models;
using Entities.Models.Conversation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdoptPet.Pages.Conversations
{
    public class MessagesModel : DI_BasePageModel
    {
        private readonly ApplicationDbContext _context;

        private readonly INotyfService _notyfService;

        public MessagesModel(ApplicationDbContext context, IAuthorizationService authorizationService, UserManager<IdentityUser> userManager, INotyfService notyfService) : base(context, authorizationService, userManager)
        {
            _context = context;
            _notyfService = notyfService;
        }

        [BindProperty]
        public IList<Message> Messages { get; set; }
        public Chat Chat { get; set; }

        public string currentUserId { get; set; }
        public string adOwnerUsername { get; set;}

        public async Task<IActionResult> OnGetAsync(Guid chatId)
        {
            currentUserId = UserManager.GetUserId(User);

            Chat = await _context.Chat.Where(c => c.Id.Equals(chatId)).SingleOrDefaultAsync();

            if(Chat==null)
            {
                _notyfService.Error("B��d w przetwarzaniu danych");
                return RedirectToPage("/Conversations/Index");
            }

            var ownerOfAdUsername = await UserManager.FindByIdAsync(Chat.Ad.OwnerId);
            adOwnerUsername = ownerOfAdUsername.UserName.Remove(ownerOfAdUsername.UserName.IndexOf("@"));

            Messages = await _context.Message.Where(m => m.ChatId.Equals(chatId)).ToListAsync();

            if (Messages.Any())
            {
                foreach (var message in Messages)
                {
                    var user = await UserManager.FindByIdAsync(message.SendByUserId);
                    message.UsernameOfSender = user.UserName.Remove(user.UserName.IndexOf("@"));
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            var message = await _context.Message.Where(m => m.Id.Equals(id)).SingleOrDefaultAsync();

            if (message == null)
            {
                _notyfService.Warning("Wewn�trzny b��d podczas usuwania.");
                return RedirectToPage("/Conversations/Messages", new { chatId = message.ChatId });
            }

             currentUserId = UserManager.GetUserId(User);

            if (message.UsernameOfSender != currentUserId)
            {
                _notyfService.Warning("Niedozwolona akcja"); // only the author of the message can delete 
                return RedirectToPage("/Conversations/Messages", new { chatId = message.ChatId });
            }

            _context.Message.Remove(message);
            await _context.SaveChangesAsync();

            _notyfService.Success("Wiadomo�� zosta�a usuni�ta.");
            return RedirectToPage("/Conversations/Messages", new { chatId = message.ChatId });
        }
    }
}
