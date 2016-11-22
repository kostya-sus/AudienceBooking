using System.Collections.Generic;
using Booking.Enums;

namespace Booking.Web.ViewModels.Audience
{
    public class AudiencesNamesViewModel
    {
        public IDictionary<Audiences, string> Names { get; set; }
    }
}