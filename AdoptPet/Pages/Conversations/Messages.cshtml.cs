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

        [BindProperty]
        public Message Message { get; set; }

        public string currentUserId { get; set; }
        public string adOwnerUsername { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid chatId)
        {
            currentUserId = UserManager.GetUserId(User);

            Chat = await _context.Chat.Where(c => c.Id.Equals(chatId)).SingleOrDefaultAsync();

            if (Chat == null)
            {
                _notyfService.Error("B³¹d w przetwarzaniu danych");
                return RedirectToPage("/Conversations/Index");
            }

            var usernameOfCreatorChat = await UserManager.FindByIdAsync(Chat.CreatedByUserId);
            Chat.UsernameOfCreator = usernameOfCreatorChat.UserName.Remove(usernameOfCreatorChat.UserName.IndexOf("@"));

            var ownerOfAdUsername = await UserManager.FindByIdAsync(Chat.Ad.OwnerId);
            adOwnerUsername = ownerOfAdUsername.UserName.Remove(ownerOfAdUsername.UserName.IndexOf("@"));

            // check unauthorized access
            if (currentUserId != Chat.CreatedByUserId && currentUserId != Chat.Ad.OwnerId)
            {
                _notyfService.Error("Nieautoryzowany dostêp!");
                return RedirectToPage("/Conversations/Index");
            }

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
                _notyfService.Warning("Wewnêtrzny b³¹d podczas usuwania.");
                return RedirectToPage("/Conversations/Messages", new { chatId = message.ChatId });
            }

            currentUserId = UserManager.GetUserId(User);

            if (message.SendByUserId != currentUserId)
            {
                _notyfService.Warning("Niedozwolona akcja"); // only the author of the message can delete 
                return RedirectToPage("/Conversations/Messages", new { chatId = message.ChatId });
            }

            _context.Message.Remove(message);
            await _context.SaveChangesAsync();

            _notyfService.Success("Wiadomoœæ zosta³a usuniêta.");
            return RedirectToPage("/Conversations/Messages", new { chatId = message.ChatId });
        }

        public async Task<IActionResult> OnPostAsync(Guid chatId)
        {
            if (!ModelState.IsValid)
            {
                _notyfService.Error("B³¹d w przetwarzaniu danych");
                return RedirectToPage("/Conversations/Messages", new { chatId = chatId });
            }

            var chat = await _context.Chat.Where(c => c.Id.Equals(chatId)).SingleOrDefaultAsync();

            if (chat == null)
            {
                _notyfService.Error("B³¹d w przetwarzaniu danych");
                return RedirectToPage("./Conversations/Index");
            }

            // change the status of the last message to just read

            currentUserId = UserManager.GetUserId(User);
            var lastMsg = chat.Messages.OrderByDescending(m => m.DateOfSending).FirstOrDefault();

            var lastMsgFromDb = _context.Message.Where(m => m.Id.Equals(lastMsg.Id));

            if (lastMsgFromDb == null)
            {
                _notyfService.Error("B³¹d w przetwarzaniu danych");
                return RedirectToPage("./Conversations/Index");
            }

            if (lastMsg.SendByUserId == currentUserId)
            {
                lastMsg.Status = Message.ChatStatus.Odczytane;

                _context.Entry(lastMsgFromDb).CurrentValues.SetValues(lastMsg);
            }

            //add a new message
            Message.ChatId = chat.Id;
            Message.DateOfSending = DateTime.Now;
            Message.SendByUserId = UserManager.GetUserId(User);
            Message.Status = Message.ChatStatus.Nieodczytane;

            _context.Message.Add(Message);

            await _context.SaveChangesAsync();

            _notyfService.Success("Poprawnie wys³ano wiadomoœæ");
            return RedirectToPage("/Conversations/Messages", new { chatId = chatId });
        }
    }
}
