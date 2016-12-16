using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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