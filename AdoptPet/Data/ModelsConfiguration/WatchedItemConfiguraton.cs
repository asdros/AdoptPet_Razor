using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace AdoptPet.Data.ModelConfiguration
{
    public class WatchedItemConfiguration : IEntityTypeConfiguration<WatchedItem>
    {
        public void Configure(EntityTypeBuilder<WatchedItem> builder)
        {
            builder.HasData
                (
                new WatchedItem
                {
                    Id =new Guid("6A183CF8-7B8E-4BEC-828C-7FD506880DDE"),
                    AdId = new Guid("25c3424d-79cd-40ca-a4e3-d71a14635141"),
                },
                new WatchedItem
                {
                    Id = new Guid("17457CFD-63F5-4F6E-9FF1-B5ADA7A4EA2A"),
                    AdId = new Guid("8e23fb77-0a52-4e65-8d52-753ba1a6be4f"),
                },
                new WatchedItem
                {
                    Id = new Guid("57050ACD-C26B-46FD-A0F7-0718B6C84EB4"),
                    AdId = new Guid("25c3424d-79cd-40ca-a4e3-d71a14635141"),
                }
                );
        }
    }
}
