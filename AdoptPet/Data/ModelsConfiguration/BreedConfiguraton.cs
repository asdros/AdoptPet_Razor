using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdoptPet.Data.ModelConfiguration
{
    public class BreedConfiguration : IEntityTypeConfiguration<Breed>
    {
        public void Configure(EntityTypeBuilder<Breed> builder)
        {
            builder.HasData
                    (
                new Breed
                {
                    Id = 1,
                    Name = "Maine coon",
                    AnimalId = 1
                },
                new Breed
                {
                    Id = 2,
                    Name = "Syjamski",
                    AnimalId = 1
                },
                new Breed
                {
                    Id = 3,
                    Name = "Owczarek",
                    AnimalId = 2
                }
                );
        }
    }
}
