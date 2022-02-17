using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace AdoptPet.Models.Configuration
{
    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasData
                    (
                        new Image
                        {
                            Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                            isPoster = true,
                            Name = "cat.jpg",
                            AdId = new Guid("8e23fb77-0a52-4e65-8d52-753ba1a6be4f")
                        },
                        new Image
                        {
                            Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991871"),
                            isPoster = true,
                            Name = "catNOP.jpg",
                            AdId = new Guid("dde9be6e-6ff8-4c9e-b844-387b7f4ec5a9")
                        },
                        new Image
                        {
                            Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991872"),
                            isPoster = true,
                            Name = "dog.jpg",
                            AdId = new Guid("25c3424d-79cd-40ca-a4e3-d71a14635141")
                        }
                );
        }
    }
}
