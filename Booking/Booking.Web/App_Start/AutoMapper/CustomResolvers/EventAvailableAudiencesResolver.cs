using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Booking.Models.EfModels;
using Booking.Web.ViewModels.Event;

namespace Booking.Web.AutoMapper.CustomResolvers
{
    public class EventAvailableAudiencesResolver : IValueResolver<Event, CreateEditEventViewModel, IDictionary<Guid, string>>
    {
        public IDictionary<Guid, string> Resolve(Event source, CreateEditEventViewModel destination, IDictionary<Guid, string> destMember,
            ResolutionContext context)
        {
            var audiences = source.Audience.AudienceMap.Audiences;
            return audiences.Where(a => a.IsBookingAvailable)
                .ToDictionary(a => a.Id, a => a.Name);
        }
    }
}