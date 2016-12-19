using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Booking.Web.ViewModels.AdminPanel
{
    public class CreateAudienceViewModel
    {
        [Required]
        public Guid AudienceMapId { get; set; }

        [Required]
        public string Name { get; set; }

        public HttpPostedFileBase RouteImage { get; set; }

        public HttpPostedFileBase LineDetailsImage { get; set; }
        
        [Required]
        public bool IsBookingAvailable { get; set; }

        [Required]
        public int Left { get; set; }

        [Required]
        public int Top { get; set; }

        [Required]
        public int Width { get; set; }

        [Required]
        public int Height { get; set; }

        [Required]
        public int SeatsCount { get; set; }

        [Required]
        public int BoardsCount { get; set; }

        [Required]
        public int LaptopsCount { get; set; }

        [Required]
        public int PrintersCount { get; set; }

        [Required]
        public int ProjectorsCount { get; set; }
    }
}