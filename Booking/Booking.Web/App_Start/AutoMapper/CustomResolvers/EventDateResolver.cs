using System.Globalization;
using AutoMapper;
using Booking.Models.EfModels;
using Booking.Web.ViewModels.Event;

namespace Booking.Web.AutoMapper.CustomResolvers
{
    public class EventDateResolver : IValueResolver<Event, DisplayEventPopupViewModel, string>
    {
        public string Resolve(Event source, DisplayEventPopupViewModel destination, string destMember,
            ResolutionContext context)
        {
            CultureInfo culture = CultureInfo.CreateSpecificCulture("ru-RU");
            return source.StartTime.ToString("ddd, d MMMM", culture);
        }
    }
}