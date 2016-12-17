using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Booking.Web.ViewModels.AdminPanel
{
    public class AdminPanelViewModel
    {
        public IDictionary<Guid, string> AudienceMaps { get; set; }

        public Guid ActiveMapId { get; set; }
    }
}