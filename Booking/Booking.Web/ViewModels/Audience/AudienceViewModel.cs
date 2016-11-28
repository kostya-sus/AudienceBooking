using System.Collections.Generic;
using Booking.Enums;

namespace Booking.Web.ViewModels.Audience
{
    public class AudienceViewModel
    {
        public AudiencesNamesViewModel AudiencesNames { get; set; }

        public IEnumerable<AudiencesEnum> AvailableAudiences { get; set; }

        public AudienceInfoViewModel SelectedAudience { get; set; }

        public bool IsOpened { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsLoggedIn { get; set; }
    }
}