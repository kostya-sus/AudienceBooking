using System;
using System.Web.Mvc;
using Booking.Web.ViewModels.Profile;

namespace Booking.Web.Controllers
{
    public class ProfileController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Schedule(string userId, DateTime date)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(string userId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Save(EditProfileViewModel editProfileViewModel)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string userId)
        {
            throw new NotImplementedException();
        }
    }
}