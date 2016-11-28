using System;
using System.Linq;
using Booking.Enums;
using Booking.Models;

namespace Booking.Repositories.Interfaces
{
    public interface IAudienceRepository : IDisposable
    {
        IQueryable<Audience> GetAllAudiences();

        Audience GetAudienceById(AudiencesEnum id);

        void UpdateAudience(Audience audience);

        void Save();
    }
}
