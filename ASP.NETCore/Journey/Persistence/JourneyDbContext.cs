using System.Diagnostics.CodeAnalysis;
using Journey.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Journey.Persistence
{
    public class JourneyDbContext : DbContext
    {
        public JourneyDbContext([NotNullAttribute] DbContextOptions options) : base(options)
        { }

        public DbSet<City> Cities { get; set; }
        public DbSet<PlaceOfInterest> InterestingPlaces { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) {
            builder.Entity<PlaceOfInterest>().ToTable("Place");

            builder.Entity<City>(cityBuilder =>
            {
                cityBuilder.ToTable("City");
                cityBuilder.HasMany(c => c.IntrestingPlaces)
                    .WithOne(c => c.CityOwner)
                    .HasForeignKey(p => p.CityOwnerId);
            });
        }
    }
}