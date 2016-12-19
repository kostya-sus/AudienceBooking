using AutoMapper;
using Booking.Models.EfModels;
using Booking.Repositories.Repositories;
using Booking.Web.ViewModels.Audience;

namespace Booking.Web.AutoMapper.CustomResolvers
{
    public class AudienceLineDetailsImageResolver : IValueResolver<Audience, AudienceInfoViewModel, string>
    {
        public string Resolve(Audience source, AudienceInfoViewModel destination, string destMember, ResolutionContext context)
        {
            if (source.LineDetailsImageName == null) return null;
            var imageRepository = new ImageBlobRepository();
            return imageRepository.GetImageUri(source.LineDetailsImageName);
        }
    }
}