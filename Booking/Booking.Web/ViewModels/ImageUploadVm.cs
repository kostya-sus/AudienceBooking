using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Booking.Web.ViewModels
{
    public class ImageUploadVm
    {
        [Required]
        public HttpPostedFileBase File { get; set; }
    }
}