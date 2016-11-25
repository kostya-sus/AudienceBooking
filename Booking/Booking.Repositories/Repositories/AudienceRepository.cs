using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Enums;
using Booking.Models;
using Booking.Repositories.Interfaces;

namespace Booking.Repositories.Repositories
{
    public class AudienceRepository : IAudienceRepository
    {
        private readonly BookingDbContext _context;

        public AudienceRepository(BookingDbContext context)
        {
            _context = context;
        }

        public IQueryable<Audience> GetAllAudiences()
        {
            return _context.Audiences;
            ////return _context.Audiences.Select(x => new Audience
            ////{
            ////    Id = x.Id,
            ////    Name = x.Name,
            ////    SeatsCount = x.SeatsCount,
            ////    BoardsCount = x.BoardsCount,
            ////    LaptopsCount = x.LaptopsCount,
            ////    PrintersCount = x.PrintersCount,
            ////    ProjectorsCount = x.ProjectorsCount,
            ////    IsBookingAvailable = x.IsBookingAvailable,
            ////    Events = x.Events
            ////});

        }

        public Audience GetAudienceById(int id)
        {
            return _context.Audiences.Find(id);
            ////return _context.Audiences.Where(x => x.Id == (Audiences) id) as Audience;
            //// return _context.Audiences.FirstOrDefault(aud => aud.Id == (Audiences) id);
            // need test!
        }

        public void UpdateAudience(Audience audience)
        {
            _context.Entry(audience).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool _disposed;

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
