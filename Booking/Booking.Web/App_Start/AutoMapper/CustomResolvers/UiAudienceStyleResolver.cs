using AutoMapper;
using Booking.Models.EfModels;
using Booking.Repositories;
using Booking.Services.Services;
using Booking.Web.ViewModels.Audience;

namespace Booking.Web.AutoMapper.CustomResolvers
{
    public class UiAudienceStyleResolver :IValueResolver<Audience, UiAudienceViewModel, string>
    {
        public string Resolve(Audience source, UiAudienceViewModel destination, string destMember, ResolutionContext context)
        {
            var uof= new UnitOfWork();
            var audienceService = new AudienceService(uof);
            return audienceService.GetStyleString(source);
        }
    }
}