using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Booking.Web.Controllers
{
    public class AdminPanelController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}