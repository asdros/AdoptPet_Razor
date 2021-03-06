using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace AdoptPet.Data.ModelConfiguration
{
    public class AdConfiguration : IEntityTypeConfiguration<Ad>
    {
        public void Configure(EntityTypeBuilder<Ad> builder)
        {
            builder.HasData
            (
                new Ad
                {
                    Id = new Guid("8e23fb77-0a52-4e65-8d52-753ba1a6be4f"),
                    Title = "Śliczny kotek",
                    NormalizedLink = "sliczny-kotek-d62d1a",
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.",
                    Sterilization = true,
                    Deworming = true,
                    AvailableFrom = DateTime.Now,
                    LastModified = null,
                    BreedId = 2,
                    PlaceId = 1,
                  //  ImageId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870")
                },
                new Ad
                {
                    Id = new Guid("25c3424d-79cd-40ca-a4e3-d71a14635141"),
                    Title = "kot Bajtek",
                    NormalizedLink = "kot-bajtek-s73hf",
                    Description = "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source.",
                    Sterilization = false,
                    Deworming = true,
                    AvailableFrom = DateTime.Now,
                    LastModified = null,
                    BreedId = 1,
                    PlaceId = 3,
                  //  ImageId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991871")
                },
                new Ad
                {
                    Id = new Guid("dde9be6e-6ff8-4c9e-b844-387b7f4ec5a9"),
                    Title = "Azorek",
                    NormalizedLink = "azorek-si83f",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. ",
                    Sterilization = true,
                    Deworming = true,
                    AvailableFrom = DateTime.Now,
                    LastModified = null,
                    BreedId = 3,
                    PlaceId = 3,
                 //   ImageId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991872")
                }
            );
        }
    }
}
