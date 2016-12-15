using System;
using System.Web.Mvc;
using Booking.Services.Services;
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
                string name = contactViewModel.Name;
                string surname = contactViewModel.Surname;
                string message = contactViewModel.Message;
                string email = contactViewModel.Email;
                //EmailNotificationService.SendFeedbackToAdmins(name, surname, email, message);
                ContactService.SendMessage(name,surname,email,message);

                ViewData["FeedbackSentMessage"] = Localization.Localization.ContactViewModel_SuccessMessage;

            }
            else
            {
                ViewData["FeedbackSentMessage"] = null;
            }

            return View();
        }
       
    }
}