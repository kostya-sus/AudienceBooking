using System.Collections.Generic;
using AutoMapper;
using Booking.Models.EfModels;
using Booking.Web.AutoMapper.CustomResolvers;
using Booking.Web.ViewModels.Event;
using Booking.Web.ViewModels.Home;

namespace Booking.Web.AutoMapper.AutoMapperProfiles
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Event, DisplayEventPopupViewModel>()
                .ForMember(dest => dest.AuthorName, opt => opt.ResolveUsing<EventAuthorNameResolver>())
                .ForMember(dest => dest.EventDate, opt => opt.ResolveUsing<EventDateResolver>())
                .ForMember(dest => dest.AudienceName, opt => opt.MapFrom(source => source.Audience.Name))
                .ForMember(dest => dest.ParticipantsCount, opt => opt.MapFrom(source => source.EventParticipants.Count));

            CreateMap<Event, DayScheduleItemViewModel>();

            CreateMap<IEnumerable<Event>, DayScheduleViewModel>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(source => source));
        }
    }
}