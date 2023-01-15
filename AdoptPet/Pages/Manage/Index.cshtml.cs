using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdoptPet.Areas.Authorization;
using AdoptPet.Data;
using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Contracts;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdoptPet.Pages.Manage
{
    [BindProperties]
    public class IndexModel : DI_BasePageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly INotyfService _notyfService;
        private readonly ILoggerManager _loggerManager;

        public IndexModel(ApplicationDbContext context, IAuthorizationService authorizationService, UserManager<IdentityUser> userManager, IMapper mapper, INotyfService notyfService, ILoggerManager loggerManager)
            : base(context, authorizationService, userManager)
        {
            _context = context;
            _mapper = mapper;
            _notyfService = notyfService;
            _loggerManager = loggerManager;
        }

        public IList<Animal> Animals { get; set; }
        public IList<BreedManagerViewDTO> Breeds { get; set; }
        public IList<UserDTO> Users { get; set; }
        public IList<UserDTO> Moderators { get; set; }

        public Breed Breed { get; set; }
        public Animal Animal { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var isAuthorized = User.IsInRole(Constants.AdministratorsRole);

            if(!isAuthorized)
            {
                return Forbid();
            }

            Animals = await _context.Animal.ToListAsync();

            var breedsFromDB = await _context.Breed.ToListAsync();

            Breeds = _mapper.Map<IList<BreedManagerViewDTO>>(breedsFromDB);

            foreach (var element in Breeds)
            {
                element.CountOfAds = _context.Ad.Where(a => a.Breed.Name.Equals(element.Name)).ToList().Count;
            }

            var managers = await UserManager.GetUsersInRoleAsync("Managers");

            var managersUserName = managers.Select(x => x.UserName).Take(50).ToList();

            // get users without any role
            var users = await UserManager.Users.Where(u => !managersUserName.Contains(u.UserName)).Take(50).ToListAsync();

            Users = _mapper.Map<IList<UserDTO>>(users);
            Moderators = _mapper.Map<IList<UserDTO>>(managers);

            return Page();
        }
        #region adminUsersManage
        public async Task<IActionResult> OnPostRemoveModeratorAsync(string email)
        {
            if(string.IsNullOrWhiteSpace(email))
            {
                _notyfService.Error("Przes³ano b³êdne dane");
                return RedirectToPage("/Manage/Index");
            }

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
            if (string.IsNullOrWhiteSpace(email))
            {
                _notyfService.Error("Przes³ano b³êdne dane");
                return RedirectToPage("/Manage/Index");
            }

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

        #region breeds&AnimalsManage

        public async Task<IActionResult> OnPostAddBreedAsync(string name, int animalId)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                _notyfService.Error("Przes³ano b³êdne dane");
                return RedirectToPage("/Manage/Index");
            }

            //check if this species already exists in the database
            var breedsFromDB = await _context.Breed
                .Where(b => b.Name.ToLower().Equals(name.ToLower()) 
                && b.AnimalId.Equals(animalId)).ToListAsync();

            if (breedsFromDB.Any())
            {
                _notyfService.Error("Dana rasa istnieje ju¿ w bazie");
                return RedirectToPage("/Manage/Index");
            }

            var speciesFromDB = await _context.Animal.Where(a => a.Id.Equals(animalId)).ToListAsync();

            if (!speciesFromDB.Any())
            {
                _notyfService.Error("Brak podanego gatunku");
                return RedirectToPage("/Manage/Index");
            }

            Breed.Name = name.ToLower();
            Breed.AnimalId = animalId;
            _context.Add(Breed);

            await _context.SaveChangesAsync();

            _loggerManager.LogInfo($"User {User.Identity.Name} added animal breed {name}");
            _notyfService.Success($"Poprawnie dodano gatunek {name}");
            return RedirectToPage("/Manage/Index");
        }

        public async Task<IActionResult> OnPostRemoveBreedAsync(string name, int animalId)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                _notyfService.Error("Przes³ano b³êdne dane");
                return RedirectToPage("/Manage/Index");
            }

            if (name == "inna")
            {
                _notyfService.Error("Niedozwolona operacja usuniêcia rasy");
                return RedirectToPage("/Manage/Index");
            }

            var animalFromDbForExistedAds = await _context.Breed.Where(b => b.AnimalId.Equals(animalId) && b.Name.Equals("inna")).FirstOrDefaultAsync();
            var breedsFromDB = await _context.Breed.Where(b => b.Name.Equals(name)).ToListAsync();

            if (!breedsFromDB.Any())
            {
                _notyfService.Error("Podana rasa nie istnieje w bazie");
                return RedirectToPage("/Manage/Index");
            }

            var breedToDelete = await _context.Breed.Where(b => b.Name.Equals(name)).SingleOrDefaultAsync();

            // change breed in existed ads to other
            var adsToUpdateFromDB = await _context.Ad.Where(a => a.Breed.Name.Equals(name)).ToListAsync();
            adsToUpdateFromDB.ForEach(a => a.Breed = animalFromDbForExistedAds);

            //delete breed from db
            _context.Breed.Remove(breedToDelete);
            await _context.SaveChangesAsync();

            _loggerManager.LogWarn($"User {User.Identity.Name} deleted animal species {name}");
            _notyfService.Success($"Poprawnie usuniêto gatunek {name}");
            return RedirectToPage("/Manage/Index");
        }

        public async Task<IActionResult> OnPostAddAnimalAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                _notyfService.Error("Przes³ano b³êdne dane");
                return RedirectToPage("/Manage/Index");
            }

            //check if this species already exists in the database
            var animalsFromDB = await _context.Animal.Where(a => a.Species.ToLower().Equals(name.ToLower())).ToListAsync();

            if (animalsFromDB.Any())
            {
                _notyfService.Error("Dana rasa istnieje ju¿ w bazie");
                return RedirectToPage("/Manage/Index");
            }

            Animal.Species = name.ToLower();
            _context.Add(Animal);

            await _context.SaveChangesAsync();

            Breed.Name = "inna";
            Breed.AnimalId = Animal.Id;

            _context.Add(Breed);

            await _context.SaveChangesAsync();

            _loggerManager.LogInfo($"User {User.Identity.Name} added animal species {name}");
            _notyfService.Success("Poprawnie dodano now¹ rasê");
            return RedirectToPage("/Manage/Index");
        }

        #endregion
    }
}
