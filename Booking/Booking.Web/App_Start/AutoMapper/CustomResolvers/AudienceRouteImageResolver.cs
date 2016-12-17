using AutoMapper;
using Booking.Models.EfModels;
using Booking.Repositories.Repositories;
using Booking.Web.ViewModels.Audience;

namespace Booking.Web.AutoMapper.CustomResolvers
{
    public class AudienceRouteImageResolver : IValueResolver<Audience, UiAudienceViewModel, string>
    {
        public string Resolve(Audience source, UiAudienceViewModel destination, string destMember,
            ResolutionContext context)
        {
            if (source.RouteImageName == null) return null;
            var imageRepository = new ImageBlobRepository();
            return imageRepository.GetImageUri(source.RouteImageName);
        }
    }
}