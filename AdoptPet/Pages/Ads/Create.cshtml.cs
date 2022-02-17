using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AdoptPet.Data;
using Microsoft.AspNetCore.Http;
using Contracts;
using Entities.Models;
using AutoMapper;

namespace AdoptPet.Pages.Ads
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILoggerManager _loggerManager;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;

        public CreateModel(ApplicationDbContext context, ILoggerManager loggerManager, IImageService imageService, IMapper mapper)
        {
            _context = context;
            _loggerManager = loggerManager;
            _imageService = imageService;
            _mapper = mapper;
        }

        public IActionResult OnGet()
        {
            ViewData["AnimalId"] = new SelectList(_context.Animal, "Id", "Species");
            ViewData["BreedId"] = new SelectList(_context.Breed, "Id", "Name");
            //    ViewData["PlaceId"] = new SelectList(_context.Place, "Id", "Id");

            return Page();
        }

        [BindProperty]
        public Ad Ad { get; set; }
        [BindProperty]
        public List<IFormFile> Images { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                          .Where(y => y.Count > 0)
                          .ToList();
                return Page();
            }

            Ad.AvailableFrom = DateTime.Now;
            Ad.NormalizedLink = Ad.GenerateLink(Ad.Title);

            _context.Ad.Add(Ad);


            await _context.SaveChangesAsync();

            if(Images.Any(f => f.Length==0))
            {
                _loggerManager.LogError("Images object sent from client is null.");
                return RedirectToPage("./Index");
            }

            foreach(var image in Images)
            {
                var imageDTO = await _imageService.SaveImageToDisk(image, Ad.Id);

                imageDTO.isPoster = Images.First() == image ? true : false;

                var imageEntity = _mapper.Map<Image>(imageDTO);

                _context.Image.Add(imageEntity);
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }

}
