using System;
using System.Linq;
using Booking.Models.EfModels;

namespace Booking.Repositories.Interfaces
{
    public interface IAudienceMapRepository : IDisposable
    {
        IQueryable<AudienceMap> GetAllAudienceMaps();

        AudienceMap GetAudienceById(Guid id);

        void CreateAudienceMap(AudienceMap audienceMap);

        void UpdateAudience(AudienceMap audienceMap);

        void DeleteAudienceMap(AudienceMap audienceMap);

        void Save();
    }
}