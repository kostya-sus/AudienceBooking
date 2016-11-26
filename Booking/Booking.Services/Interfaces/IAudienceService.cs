using System.Collections.Generic;
using Booking.Enums;
using Booking.Models;

namespace Booking.Services.Interfaces
{
    public interface IAudienceService
    {
        Audience GetAudience(int audienceId);

        bool EditAudience(Audience audience);

        bool CloseAudience(int audienceId);

        bool OpenAudience(int audienceId);

        IEnumerable<Audience> GetAllAudiences();

        IEnumerable<Audiences> GetAvailableAudiencesIds();
    }
}