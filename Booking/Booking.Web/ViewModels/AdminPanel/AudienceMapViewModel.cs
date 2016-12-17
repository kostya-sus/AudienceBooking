using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Booking.Web.ViewModels.AdminPanel
{
    public class AudienceMapViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public IEnumerable<Models.EfModels.Audience> Audiences { get; set; }
    }
}