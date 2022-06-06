using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Entities.Models.Conversation;

namespace AdoptPet.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Image>()
                .HasOne<Ad>(i => i.Ad)
                .WithMany(a => a.Images)
                .HasForeignKey(i => i.AdId)
                .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.ApplyConfiguration(new DistrictConfiguration());
            //modelBuilder.ApplyConfiguration(new ProvinceConfiguration());
            //modelBuilder.ApplyConfiguration(new PlaceConfiguration());

            //modelBuilder.ApplyConfiguration(new AdConfiguration());
            //modelBuilder.ApplyConfiguration(new AnimalConfiguration());
            //modelBuilder.ApplyConfiguration(new BreedConfiguration());
            //modelBuilder.ApplyConfiguration(new ImageConfiguration());
            //modelBuilder.ApplyConfiguration(new WatchedItemConfiguration());
        }

        public DbSet<Province> Province { get; set; }
        public DbSet<District> District { get; set; }
        public DbSet<Place> Place { get; set; }

        public DbSet<Ad> Ad { get; set; }
        public DbSet<Animal> Animal { get; set; }
        public DbSet<Breed> Breed { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<WatchedItem> WatchedItem { get; set; }

        public DbSet<Chat> Chat { get; set; }
        public DbSet<Message> Message { get; set; }
    }
}
