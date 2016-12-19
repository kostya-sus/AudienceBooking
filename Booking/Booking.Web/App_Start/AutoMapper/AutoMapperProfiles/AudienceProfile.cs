using AutoMapper;
using Booking.Models.EfModels;
using Booking.Web.AutoMapper.CustomResolvers;
using Booking.Web.ViewModels.Audience;

namespace Booking.Web.AutoMapper.AutoMapperProfiles
{
    public class AudienceProfile : Profile
    {
        public AudienceProfile()
        {
            CreateMap<Audience, UiAudienceViewModel>()
                .ForMember(dest => dest.Style, opt => opt.ResolveUsing<UiAudienceStyleResolver>())
                .ForMember(dest => dest.RouteImageUrl, opt=>opt.ResolveUsing<AudienceRouteImageResolver>());

            CreateMap<Audience, AudienceInfoViewModel>()
                .ForMember(dest => dest.LineDetailsImageUrl, opt => opt.ResolveUsing<AudienceLineDetailsImageResolver>());

            CreateMap<Audience, AudienceIndexViewModel>()
                .ForMember(dest => dest.AudienceMap, opt => opt.MapFrom(source => source.AudienceMap))
                .ForMember(dest => dest.AudienceInfo, opt => opt.MapFrom(source => source));
        }
    }
}