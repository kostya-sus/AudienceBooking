using AutoMapper;
using Booking.Models.EfModels;
using Booking.Repositories.Repositories;
using Booking.Web.ViewModels.Audience;

namespace Booking.Web.AutoMapper.CustomResolvers
{
    public class AudienceLineDetailsResolver : IValueResolver<Audience, AudienceInfoViewModel, string>
    {
        public string Resolve(Audience source, AudienceInfoViewModel destination, string destMember, ResolutionContext context)
        {
            var imageRepository = new ImageBlobRepository();
            return imageRepository.GetImageUri(source.LineDetailsImageName);
        }
    }
}