using System;
using System.Collections.Generic;
using System.Linq;
using Booking.Enums;
using Booking.Models;
using Booking.Web.ViewModels.Audience;

namespace Booking.Web.Helpers
{
    public static class ViewModelMapper
    {
        public static IDictionary<AudiencesEnum, AudienceMapItemVm> ToVmDictionary(this IEnumerable<Audience> audiences)
        {
            return audiences.ToDictionary(
                a => a.Id,
                a => new AudienceMapItemVm
                {
                    Id = a.Id,
                    IsAvailable = a.IsBookingAvailable,
                    Name = a.Name
                });
        }

        public static IDictionary<Guid, string> ToVmDictionary(this IEnumerable<EventParticipant> participants)
        {
            return participants.ToDictionary(a => a.Id, a => a.ParticipantEmail);
        }
    }
}