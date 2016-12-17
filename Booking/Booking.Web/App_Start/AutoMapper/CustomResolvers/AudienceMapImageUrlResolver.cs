using AutoMapper;
using Booking.Models.EfModels;
using Booking.Repositories.Repositories;
using Booking.Web.ViewModels.AudienceMap;

namespace Booking.Web.AutoMapper.CustomResolvers
{
    public class AudienceMapImageUrlResolver : IValueResolver<AudienceMap, AudienceMapViewModel, string>
    {
        public string Resolve(AudienceMap source, AudienceMapViewModel destination, string destMember,
            ResolutionContext context)
        {
            if (source.ImageName == null) return null;
            var imageRepository = new ImageBlobRepository();
            return imageRepository.GetImageUri(source.ImageName);
        }
    }
}