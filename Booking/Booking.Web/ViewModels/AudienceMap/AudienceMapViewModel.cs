using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Booking.Web.ViewModels.Audience;

namespace Booking.Web.ViewModels.AudienceMap
{
    public class AudienceMapViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public IEnumerable<UiAudienceViewModel> Audiences { get; set; }
    }
}