using System;
using System.Web.Mvc;
using Booking.Web.ViewModels.Contact;

namespace Booking.Web.Controllers
{
    public class ContactController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var viewModel = new ContactViewModel();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(ContactViewModel contactViewModel)
        {
            if (ModelState.IsValid)
            {
                //add feedback 
                ViewData["UserMessage"] = Localization.Localization.ContactViewModel_SucsessMessage;

            }
            else
            {
                ViewData["UserMessage"] = "";
            }

            return View();
        }
       
    }
}