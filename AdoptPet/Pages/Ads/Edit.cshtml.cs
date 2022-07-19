using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdoptPet.Data;
using Entities.Models;
using AutoMapper;
using Entities.DTO;
using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using AdoptPet.Areas.Authorization;

namespace AdoptPet.Pages.Ads
{
    public class EditModel : DI_BasePageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;
        private readonly ILoggerManager _loggerManager;

        public EditModel(ApplicationDbContext context, IAuthorizationService authorizationService, UserManager<IdentityUser> userManager, IMapper mapper, IImageService imageService, ILoggerManager loggerManager)
            : base(context, authorizationService, userManager)
        {
            _context = context;
            _mapper = mapper;
            _imageService = imageService;
            _loggerManager = loggerManager;
        }

        [BindProperty]
        public AdForUpdateDTO Ad { get; set; }

        public Ad AdFromDB { get; set; }

        [BindProperty]
        public List<Image> Images { get; set; }

        public PlaceDTO PlaceToView { get; set; }

        public List<IFormFile> ImagesFromForm { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AdFromDB = await _context.Ad
                .Include(a => a.Breed)
                .Include(a => a.Place).FirstOrDefaultAsync(m => m.Id == id);

            if (AdFromDB == null)
            { 
                return NotFound();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(User, AdFromDB, UserOperations.Update);

            if(!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            if(AdFromDB.Status == AdStatus.Odrzucone)
            {
                return Forbid();
            }

            Images = await _context.Image.Where(i => i.AdId.Equals(id))
                                                    .OrderBy(i => i.isPoster)
                                                    .ToListAsync();

            if (AdFromDB == null)
            {
                return NotFound();
            }

            ViewData["BreedId"] = new SelectList(_context.Breed.Where(b => b.AnimalId.Equals(AdFromDB.Breed.AnimalId)), "Id", "Name");
            //       ViewData["PlaceId"] = new SelectList(_context.Place, "Id", "Name");
            PlaceToView = _mapper.Map<PlaceDTO>(AdFromDB.Place);

            Ad = _mapper.Map<AdForUpdateDTO>(AdFromDB); // map to send only modifiable  fields 

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Ad.LastModified = DateTime.Now;

            AdFromDB = await _context.Ad
                .Include(a => a.Breed)
                .Include(a => a.Images.Where(i => i.isPoster.Equals(false)))
                .Include(a => a.Place).FirstOrDefaultAsync(m => m.Id == Ad.Id);

            Ad.OwnerId = AdFromDB.OwnerId;

            if (AdFromDB == null)
            {
                return NotFound();
            }

            if(Ad.Status==AdStatus.Zatwierdzone)
            {
                var canApprove = await AuthorizationService.AuthorizeAsync(User, Ad, UserOperations.Approve);

                if(!canApprove.Succeeded)
                {
                    Ad.Status = AdStatus.Oczekujące;
                }
            }

            _context.Entry(AdFromDB).CurrentValues.SetValues(Ad);

            await _context.SaveChangesAsync();

            if (ImagesFromForm.Any(f => f.Length == 0))
            {
                _loggerManager.LogError("Some image object sent from client (edit ad form) is null.");
                return RedirectToPage("./Index");
            }

            foreach (var image in ImagesFromForm)
            {
                var imageDTO = await _imageService.SaveImageToDisk(image, AdFromDB.Id);

                imageDTO.isPoster = false;      // poster cannot be updated

                var imageEntity = _mapper.Map<Image>(imageDTO);

                _context.Image.Add(imageEntity);
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private bool AdExists(Guid id)
        {
            return _context.Ad.Any(e => e.Id == id);
        }

        public async Task<IActionResult> OnPostDeleteImageAsync(Guid id)
        {
            var imageFromDb = await _context.Image.Where(i => i.Id.Equals(id)).SingleOrDefaultAsync();

            if (imageFromDb != null)
            {
                _imageService.DeleteImage(imageFromDb);
                _context.Image.Remove(imageFromDb);
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("./Edit", new { id = imageFromDb.AdId });
        }
    }
}
