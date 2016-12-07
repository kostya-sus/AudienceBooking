using Booking.Web.ViewModels.Audience;

namespace Booking.Web.ViewModels
{
    public class ScheduleTableViewModel
    {
        public AudiencesNamesViewModel AvailableAudiences { get; set; }

        public int LowerHourBound { get; set; }

        public int UpperHourBound { get; set; }
    }
}