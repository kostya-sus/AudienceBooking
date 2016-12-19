using System;
using System.Collections.Generic;
using Booking.Models.EfModels;

namespace Booking.Services.Interfaces
{
    public interface IAudienceService
    {
        Audience GetAudience(Guid audienceId);

        void CreateAudience(Audience audience);

        void UpdateAudience(Audience audience);

        void DeleteAudienceById(Guid id);

        void CloseAudience(Guid audienceId);

        void OpenAudience(Guid audienceId);

        bool IsFree(Guid audienceId, DateTime eventStart, DateTime eventEnd, Guid? eventId);

        IEnumerable<Audience> GetAllAudiences();

        string GetStyleString(Audience audience);
    }
}