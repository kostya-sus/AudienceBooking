using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Booking.Web.ViewModels.Home;

namespace Booking.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var viewModel = new HomeViewModel();

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult GetDatSchedule(DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}