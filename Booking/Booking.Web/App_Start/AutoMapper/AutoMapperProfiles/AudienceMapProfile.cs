using AutoMapper;
using Booking.Models.EfModels;
using Booking.Web.AutoMapper.CustomResolvers;
using Booking.Web.ViewModels.AudienceMap;

namespace Booking.Web.AutoMapper.AutoMapperProfiles
{
    public class AudienceMapProfile : Profile
    {
        public AudienceMapProfile()
        {
            CreateMap<AudienceMap, AudienceMapViewModel>()
                .ForMember(dest => dest.ImageUrl, opt => opt.ResolveUsing<AudienceMapImageUrlResolver>());
        }
    }
}