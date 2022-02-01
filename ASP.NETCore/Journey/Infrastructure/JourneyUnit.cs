using System;
using System.Threading.Tasks;
using Journey.Persistence;
using Journey.Persistence.Entities;

namespace Journey.Infrastructure
{
    public class JourneyUnit : IDisposable
    {
        private readonly Repository<City> _cities;
        private readonly Repository<PlaceOfInterest> _intrestingPlaces;
        private readonly JourneyDbContext _context;
        public JourneyUnit(JourneyDbContext context)
        {
            _context = context;
            _cities = new Repository<City>(_context);
            _intrestingPlaces = new Repository<PlaceOfInterest>(_context);
        }

        public Repository<City> Cities => _cities;
        public Repository<PlaceOfInterest> IntrestringPlaces => _intrestingPlaces;

        public void Save()
        {
            _context.SaveChanges();
        }

        public Task SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}