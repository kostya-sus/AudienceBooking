using System;
using System.Data.Entity;
using System.Linq;
using Booking.Models;
using Booking.Models.EfModels;
using Booking.Repositories.Interfaces;

namespace Booking.Repositories.Repositories
{
    public class AudienceMapRepository : IAudienceMapRepository
    {
        private readonly BookingDbContext _context;
        private bool _disposed;

        public AudienceMapRepository(BookingDbContext context)
        {
            _context = context;
        }

        public IQueryable<AudienceMap> GetAllAudienceMaps()
        {
            return _context.AudienceMaps;
        }

        public AudienceMap GetAudienceById(Guid id)
        {
            return _context.AudienceMaps.Find(id);
        }

        public void CreateAudienceMap(AudienceMap audienceMap)
        {
            _context.AudienceMaps.Add(audienceMap);
        }

        public void UpdateAudience(AudienceMap audienceMap)
        {
            _context.Entry(audienceMap).State = EntityState.Modified;
        }

        public void DeleteAudienceMap(AudienceMap audienceMap)
        {
            _context.AudienceMaps.Remove(audienceMap);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}