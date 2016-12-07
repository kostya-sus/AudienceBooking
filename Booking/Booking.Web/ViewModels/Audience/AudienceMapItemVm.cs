using Booking.Enums;

namespace Booking.Web.ViewModels.Audience
{
    public class AudienceMapItemVm
    {
        public AudiencesEnum Id { get; set; }

        public string Name { get; set; }

        public bool IsAvailable { get; set; }
    }
}