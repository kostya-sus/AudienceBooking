using Booking.Enums;

namespace Booking.Web.ViewModels.Audience
{
    public class AudienceInfoViewModel
    {
        public Audiences Id { get; set; }

        public string Name { get; set; }

        public int SeatsCount { get; set; }

        public int BoardsCount { get; set; }

        public int LaptopsCount { get; set; }

        public int PrintersCount { get; set; }

        public int ProjectorsCount { get; set; }

        public bool IsBookingAvailable { get; set; }
    }
}