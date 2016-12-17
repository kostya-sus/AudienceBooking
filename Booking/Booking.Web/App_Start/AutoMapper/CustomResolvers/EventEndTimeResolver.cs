using System;
using AutoMapper;
using Booking.Models.EfModels;

namespace Booking.Web.AutoMapper.CustomResolvers
{
    public class EventEndTimeResolver<T> : IValueResolver<Event, T, DateTime>
    {
        public DateTime Resolve(Event source, T destination, DateTime destMember, ResolutionContext context)
        {
            return source.StartTime.AddMinutes(source.Duration);
        }
    }
}