using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdoptPet.Models.Configuration
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
