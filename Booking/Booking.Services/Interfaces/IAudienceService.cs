using System;
using System.Collections.Generic;
using Booking.Enums;
using Booking.Models;

namespace Booking.Services.Interfaces
{
    public interface IAudienceService
    {
        Audience GetAudience(int audienceId);

        void UpdateAudience(Audience audience);

        void CloseAudience(int audienceId);

        void OpenAudience(int audienceId);

        bool IsFree(int audienceId, DateTime dateTime, int duration);

        IEnumerable<Audience> GetAllAudiences();

        IEnumerable<AudiencesEnum> GetAvailableAudiencesIds();

        IDictionary<AudiencesEnum, string> GetAllAudiencesNames();

        IDictionary<AudiencesEnum, string> GetAvailableAudiencesNames();
    }
}