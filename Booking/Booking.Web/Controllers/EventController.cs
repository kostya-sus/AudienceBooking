using System;
using System.Web.Mvc;
using Booking.Web.ViewModels.Event;

namespace Booking.Web.Controllers
{
    public class EventController : Controller
    {
        [HttpGet]
        public ActionResult DisplayEventPopup(Guid eventId)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public ActionResult Index(Guid eventId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public ActionResult Participate(string email)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Authorize]
        public ActionResult RemoveParticipant(string email)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create(Guid audienceId, DateTime eventDateTime, int duration)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateEditEventViewModel createEditEventViewModel)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Cancel(Guid eventId)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Authorize]
        public ActionResult EditPopup(Guid eventId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Save(CreateEditEventViewModel createEditEventViewModel)
        {
            throw new NotImplementedException();
        }
    }
}