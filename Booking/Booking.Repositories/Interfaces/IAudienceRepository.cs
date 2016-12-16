using System;
using System.Linq;
using Booking.Enums;
using Booking.Models;
using Booking.Models.EfModels;

namespace Booking.Repositories.Interfaces
{
    public interface IAudienceRepository : IDisposable
    {
        IQueryable<Audience> GetAllAudiences();

        Audience GetAudienceById(Guid id);
        
        void UpdateAudience(Audience audience);
        
        void Save();
    }
}
