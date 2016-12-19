using System;
using System.Collections.Generic;
using Booking.Models.EfModels;

namespace Booking.Services.Interfaces
{
    public interface IAudienceMapService
    {
        IEnumerable<AudienceMap> GetAllAudienceMaps();

        AudienceMap GetAudienceMap(Guid id);

        bool Exists(Guid id);

        void CreateAudienceMap(AudienceMap audienceMap);

        void DeleteAudienceMap(AudienceMap audienceMap);
    }
}
