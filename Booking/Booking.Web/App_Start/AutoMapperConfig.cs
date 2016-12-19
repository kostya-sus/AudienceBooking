using AutoMapper;
using Booking.Web.AutoMapper.AutoMapperProfiles;

namespace Booking.Web
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<EventProfile>();
                cfg.AddProfile<AudienceProfile>();
                cfg.AddProfile<AudienceMapProfile>();
            });
        }
    }
}