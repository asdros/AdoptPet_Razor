using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdoptPet.Data.ModelConfiguration
{
    class DistrictConfiguration : IEntityTypeConfiguration<District>
    {
        public void Configure(EntityTypeBuilder<District> builder)
        {
            builder.HasData
                (
                    new District
                    {
                        Id = 1,
                        Name = "złotowski",
                        ProvinceId = 1
                    },
                    new District
                    {
                        Id = 2,
                        Name = "pilski",
                        ProvinceId = 1
                    },
                    new District
                    {
                        Id = 3,
                        Name = "płocki",
                        ProvinceId = 2
                    }
                    );
        }
    }
}
