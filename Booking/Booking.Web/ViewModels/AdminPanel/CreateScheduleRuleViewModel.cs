using System;
using System.ComponentModel.DataAnnotations;

namespace Booking.Web.ViewModels.AdminPanel
{
    public class CreateScheduleRuleViewModel
    {
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime AppliedDate { get; set; }

        public bool ForSunday { get; set; }

        public bool ForMonday { get; set; }

        public bool ForTuesday { get; set; }

        public bool ForWednesday { get; set; }

        public bool ForThursday { get; set; }

        public bool ForFriday { get; set; }

        public bool ForSaturday { get; set; }

        [Range(0, 24)]
        public int StartHour { get; set; }

        [Range(0, 24)]
        public int EndHour { get; set; }
    }
}