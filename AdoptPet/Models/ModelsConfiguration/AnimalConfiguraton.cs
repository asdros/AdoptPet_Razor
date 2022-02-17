using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdoptPet.Models.Configuration
{
    public class AnimalConfiguration : IEntityTypeConfiguration<Animal>
    {
        public void Configure(EntityTypeBuilder<Animal> builder)
        {
            builder.HasData
                (
                    new Animal
                    {
                        Id = 1,
                        Species = "kot"
                    },
                    new Animal
                    {
                        Id = 2,
                        Species = "pies"
                    }
                );
        }
    }
}
