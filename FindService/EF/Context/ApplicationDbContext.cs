using Microsoft.EntityFrameworkCore;

namespace FindService.EF.Context
{
    //Add-Migration InitialCreate
    //Update-Database

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<AdvertisementType> AdvertisementTypes { get; set; }
        public DbSet<AdvertisementPhoto> AdvertisementPhotos { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<AdvertisementCity> AdvertisementCities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}