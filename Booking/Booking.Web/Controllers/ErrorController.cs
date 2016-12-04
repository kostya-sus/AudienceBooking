using System.Web.Mvc;
using Booking.Web.ViewModels;

namespace Booking.Web.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Error(string message, int statusCode)
        {
            return View("Error", new ErrorViewModel {StatusCode = statusCode, ErrorMessage = message});
        }
    }
}