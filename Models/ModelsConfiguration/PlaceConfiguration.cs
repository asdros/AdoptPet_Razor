using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdoptPet.Models.Configuration
{
    public class PlaceConfiguration : IEntityTypeConfiguration<Place>
    {
        public void Configure(EntityTypeBuilder<Place> builder)
        {
            builder.HasData
                (
                new Place
                {
                    Id = 1,
                    Name = "Złotów",
                    DistrictId = 1
                },
                  new Place
                  {
                      Id = 2,
                      Name = "Piła",
                      DistrictId = 1
                  }, new Place
                  {
                      Id = 3,
                      Name = "Płock",
                      DistrictId = 2
                  }
                );
        }
    }
}
