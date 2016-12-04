using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Booking.Web.Helpers;
using Booking.Web.ViewModels.Home;

namespace Booking.Web.Controllers
{
    [HandleException]
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var viewModel = new HomeViewModel();
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult GetDaySchedule(DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}