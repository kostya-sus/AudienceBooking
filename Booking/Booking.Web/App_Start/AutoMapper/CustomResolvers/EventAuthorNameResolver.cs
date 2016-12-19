using AutoMapper;
using Booking.Models;
using Booking.Models.EfModels;
using Booking.Web.ViewModels.Event;

namespace Booking.Web.AutoMapper.CustomResolvers
{
    public class EventAuthorNameResolver<T> : IValueResolver<Event, T, string>
    {
        public string Resolve(Event source, T destination, string destMember, ResolutionContext context)
        {
            return source.IsAuthorShown ? source.Author.UserName : source.AuthorName;
        }
    }
}