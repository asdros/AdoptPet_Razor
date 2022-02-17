using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IImageService
    {
        Task<ImageForCreationDTO> SaveImageToDisk(IFormFile formFile, Guid adId);
        void DeleteImage(Image image);
    }
}
