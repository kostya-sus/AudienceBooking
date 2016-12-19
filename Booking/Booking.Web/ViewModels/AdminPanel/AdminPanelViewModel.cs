using System;
using System.Collections.Generic;
using Booking.Models.EfModels;

namespace Booking.Web.ViewModels.AdminPanel
{
    public class AdminPanelViewModel
    {
        public IDictionary<Guid, string> AudienceMaps { get; set; }

        public Guid ActiveMapId { get; set; }

        public IEnumerable<BookingScheduleRule> ScheduleRules { get; set; }
    }
}