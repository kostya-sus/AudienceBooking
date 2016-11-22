using System;
using System.Web.Mvc;
using Booking.Web.ViewModels.Audience;

namespace Booking.Web.Controllers
{
    public class AudienceController : Controller
    {
        [HttpGet]
        public ActionResult Index(int audienceId)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public ActionResult GetAudienceInfo(int audienceId)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public ActionResult GetSchedule(int audienceId, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public ActionResult IsFree(int audienceId, DateTime dateTime, int duration)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Open(int audienceId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Close(int audienceId)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int audienceId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Save(AudienceInfoViewModel audienceInfoViewModel)
        {
            throw new NotImplementedException();
        }
    }
}