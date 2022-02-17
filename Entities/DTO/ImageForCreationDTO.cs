using System;

namespace Entities.DTO
{
    public class ImageForCreationDTO
    {
        public bool isPoster { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        public Guid AdId { get; set; }
    }
}
