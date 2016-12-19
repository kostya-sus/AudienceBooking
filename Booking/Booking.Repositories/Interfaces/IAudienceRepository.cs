using System;
using System.Linq;
using Booking.Models.EfModels;

namespace Booking.Repositories.Interfaces
{
    public interface IAudienceRepository : IDisposable
    {
        IQueryable<Audience> GetAllAudiences();

        Audience GetAudienceById(Guid id);

        void CreateAudience(Audience audience);

        void UpdateAudience(Audience audience);

        void DeleteAudienceById(Guid id);
        
        void Save();
    }
}
