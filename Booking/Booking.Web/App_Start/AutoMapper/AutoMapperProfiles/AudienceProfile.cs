using AutoMapper;
using Booking.Models.EfModels;
using Booking.Web.AutoMapper.CustomResolvers;
using Booking.Web.ViewModels.Audience;

namespace Booking.Web.AutoMapper.AutoMapperProfiles
{
    public class AudienceProfile:Profile
    {
        public AudienceProfile()
        {
            CreateMap<Audience, UiAudienceViewModel>()
                .ForMember(dest => dest.Style, opt => opt.ResolveUsing<UiAudienceStyleResolver>());
        }
    }
}