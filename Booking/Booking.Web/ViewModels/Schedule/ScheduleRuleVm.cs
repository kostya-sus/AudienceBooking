namespace Booking.Web.ViewModels.Schedule
{
    public class ScheduleRuleVm
    {
        public bool IsBookingAvailable { get; set; }

        public int StartHour { get; set; }

        public int EndHour { get; set; }
    }
}