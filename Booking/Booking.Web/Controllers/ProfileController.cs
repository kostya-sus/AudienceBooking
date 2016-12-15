using System;
using System.Linq;
using System.Web.Mvc;
using Booking.Repositories;
using Booking.Repositories.Interfaces;
using Booking.Services.Interfaces;
using Booking.Services.Services;
using Booking.Web.ViewModels.Manage;
using Booking.Web.ViewModels.Profile;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Booking.Web.Controllers
{
    public class ProfileController : Controller
    {
        
        [HttpGet]
        public ActionResult Index(string userId)
        {
            var vm = new ProfileViewModel()
            {
                UserInfo = new UserInfoViewModel { },
                ChangePasswordForm = new ChangePasswordViewModel { },
                Id = userId,
                IsAdmin = false,
                IsOwner = false
               
            };
            return View(vm);
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
            var vm = new EditProfileViewModel
            {
                Email = null,
                IsEditorAdmin = false,
                IsProfileAdmin = false,
                Name = null
            };

            return PartialView("_EditProfilePartial", vm);
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