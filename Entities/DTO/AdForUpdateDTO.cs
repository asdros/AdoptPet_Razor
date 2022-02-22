﻿using System;

namespace Entities.DTO
{
    public class AdForUpdateDTO
    {   
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Sterilization { get; set; }
        public bool Deworming { get; set; }
        public DateTime LastModified { get; set; }
        public int BreedId { get; set; }
        public int PlaceId { get; set; }
    }
}

