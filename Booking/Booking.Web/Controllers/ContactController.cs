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
            throw new NotImplementedException();
        }

        [HttpPost]
        public ActionResult Index(ContactViewModel contactViewModel)
        {
            throw new NotImplementedException();
        }
        [HttpGet]
        public ActionResult Contact()
        {
            var viewModel = new ContactViewModel();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Contact(ContactViewModel model)
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