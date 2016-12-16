using AutoMapper;
using Booking.Models;
using Booking.Web.ViewModels.Event;

namespace Booking.Web.AutoMapper.CustomResolvers
{
    public class EventAuthorNameResolver : IValueResolver<Event, DisplayEventPopupViewModel, string>
    {
        public string Resolve(Event source, DisplayEventPopupViewModel destination, string destMember, ResolutionContext context)
        {
            return source.IsAuthorShown ? source.Author.UserName : source.AuthorName;
        }
    }
}