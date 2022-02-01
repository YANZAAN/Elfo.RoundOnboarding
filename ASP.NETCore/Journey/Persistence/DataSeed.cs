using System.Linq;
using Journey.Persistence.Entities;

namespace Journey.Persistence
{
    public class DataSeed
    {
        private readonly JourneyDbContext _context;
        public DataSeed(JourneyDbContext context)
        {
            _context = context;
        }

        public void SeedDatabase()
        {
            if (!_context.Cities.Any())
            {
                _context.Cities.AddRange(
                    new City { Name = "San Francisco", Country = "USA", IntrestingPlaces = new [] {
                        new PlaceOfInterest { Name = "Golden Gate Bridge" },
                        new PlaceOfInterest { Name = "Golden Gate Park" },
                        new PlaceOfInterest { Name = "Twin Peaks" }
                    } },
                    new City { Name = "Sydney", Country = "Australia", IntrestingPlaces = new [] {
                        new PlaceOfInterest { Name = "Sydney Opera House" },
                        new PlaceOfInterest { Name = "Darling Harbour" }
                    } },
                    new City { Name = "Bangkok", Country = "Thailand", IntrestingPlaces = new [] {
                        new PlaceOfInterest { Name = "Grand Palace" },
                        new PlaceOfInterest { Name = "Wat Traimit, Temple of the Golden Buddha" }
                    }}
                );
            }
        }
    }
}