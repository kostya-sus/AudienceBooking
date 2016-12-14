using System;
using System.Collections.Generic;
using Booking.Enums;
using Booking.Models;

namespace Booking.Services.Interfaces
{
    public interface IAudienceService
    {
        Audience GetAudience(AudiencesEnum audienceId);

        void UpdateAudience(Audience audience);

        void CloseAudience(AudiencesEnum audienceId);

        void OpenAudience(AudiencesEnum audienceId);

        bool IsFree(AudiencesEnum audienceId, DateTime dateTime, int duration, Guid eventId=default(Guid));

        IEnumerable<Audience> GetAllAudiences();

        IEnumerable<Audience> GetAvailableAudiences();
    }
}