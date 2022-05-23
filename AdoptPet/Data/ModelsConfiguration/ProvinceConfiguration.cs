using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdoptPet.Data.ModelConfiguration
{
    public class ProvinceConfiguration : IEntityTypeConfiguration<Province>
    {
        public void Configure(EntityTypeBuilder<Province> builder)
        {
            builder.HasData
                (
                    new Province
                    {
                        Id = 1,
                        Name = "wielkopolskie"
                    },
                    new Province
                    {
                        Id = 2,
                        Name = "mazowiecki"
                    }
                );
        }
    }
}
