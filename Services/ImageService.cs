using Contracts;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Services
{
    public class ImageService : IImageService
    {
        private readonly string _imagesDirPath;
        private readonly ILoggerManager _loggerManager;

        public ImageService(ILoggerManager loggerManager)
        {
            _imagesDirPath = "wwwroot\\images";
            _loggerManager = loggerManager;
        }

        public void DeleteImage(Image image)
        {
            int index = image.Path.LastIndexOf("\\");
            var galleryPath = image.Path.Substring(0,index);
            if (File.Exists(image.Path))
            {
                File.Delete(image.Path);
            }
            
            if(Directory.GetFiles(galleryPath).Length == 0)
            {
                Directory.Delete(galleryPath);
            }
        }

        public async Task<ImageForCreationDTO> SaveImageToDisk(IFormFile formFile, Guid adId)
        {
            var fileName = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName.Trim('"');
            var dirForImages = Path.Combine(_imagesDirPath, adId.ToString());
            var adDirectory ="";

            if (!Directory.Exists(dirForImages))
            {
                adDirectory = Directory.CreateDirectory(dirForImages).ToString();
            }
            else
            {
                _loggerManager.LogError($"Cannot create new directory for ad's images. AdId: {adId}");
                adDirectory = dirForImages;
            }

            var fullPathImage = Path.Combine(adDirectory, fileName);

            using (var stream = new FileStream(fullPathImage, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }

            return new ImageForCreationDTO { Name = fileName, AdId = adId, Path=fullPathImage};
        }
    }
}
