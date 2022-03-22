using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdoptPet.Data;
using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdoptPet.Pages.Manage
{
    [BindProperties]
    public class IndexModel : DI_BasePageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly INotyfService _notyfService;

        public IndexModel(ApplicationDbContext context, IAuthorizationService authorizationService, UserManager<IdentityUser> userManager, IMapper mapper, INotyfService notyfService)
            : base(context, authorizationService, userManager)
        {
            _context = context;
            _mapper = mapper;
            _notyfService = notyfService;
        }

        public Animal Animal { get; set; }
        public Breed Breed { get; set; }
        public IList<UserDTO> Users { get; set; }
        public IList<UserDTO> Moderators { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var managers = await UserManager.GetUsersInRoleAsync("Managers");

            var managersUserName = managers.Select(x => x.UserName);
            var users = UserManager.Users.Where(u => !managersUserName.Contains(u.UserName)).ToList();

            Users = _mapper.Map<IList<UserDTO>>(users);
            Moderators = _mapper.Map<IList<UserDTO>>(managers);

            return Page();
        }
        #region adminUsersManage
        public async Task<IActionResult> OnPostRemoveModeratorAsync(string email)
        {
            var user = await UserManager.FindByNameAsync(email);

            if (user == null)
            {
                _notyfService.Error($"Nie znaleziono u¿ytkownika {email}");
                return RedirectToPage("/Manage/Index");
            }

            var result = await UserManager.RemoveFromRoleAsync(user, "Managers");

            if (!result.Succeeded)
            {
                _notyfService.Error($"U¿ytkownik {email} nie posiada uprawnieñ");
                return RedirectToPage("/Manage/Index");
            }

            await _context.SaveChangesAsync();

            _notyfService.Warning("Usuniêto moderatora");
            return RedirectToPage("/Manage/Index");
        }

        public async Task<IActionResult> OnPostAddModeratorAsync(string email)
        {
            var user = await UserManager.FindByNameAsync(email);

            if (user == null)
            {
                _notyfService.Error($"Nie znaleziono u¿ytkownika {email}");
                return RedirectToPage("/Manage/Index");
            }

            var result = await UserManager.AddToRoleAsync(user, "Managers");

            if (!result.Succeeded)
            {
                _notyfService.Error($"U¿ytkownik {email} posiada ju¿ uprawnienia");
                return RedirectToPage("/Manage/Index");
            }

            await _context.SaveChangesAsync();

            _notyfService.Success("Dodano moderatora");
            return RedirectToPage("/Manage/Index");
        }
        #endregion


    }
}
