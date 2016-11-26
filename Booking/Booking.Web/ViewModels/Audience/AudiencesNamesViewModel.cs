using System.Collections.Generic;
using Booking.Enums;

namespace Booking.Web.ViewModels.Audience
{
    public class AudiencesNamesViewModel
    {
        public IDictionary<AudiencesEnum, string> Names { get; set; }
    }
}