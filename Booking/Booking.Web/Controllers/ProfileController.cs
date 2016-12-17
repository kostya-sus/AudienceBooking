using System;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web.Mvc;
using Booking.Models;
using Booking.Repositories;
using Booking.Repositories.Interfaces;
using Booking.Services.Interfaces;
using Booking.Services.Services;
using Booking.Web.ViewModels.Manage;
using Booking.Web.ViewModels.Profile;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Threading.Tasks;

namespace Booking.Web.Controllers
{
    public class ProfileController : Controller
    {
        [HttpGet]
        public ActionResult Index(string userId)
        {
            var user = new SheduleService().GetUserById(userId);
            var vm = new ProfileViewModel()
            {
                UserInfo = new UserInfoViewModel
                {
                    IsProfileAdmin = new SheduleService().IsAdmin(user),
                    Name = user.UserName,
                    Email = user.Email,
                    ActiveEventsCount = new SheduleService().GetEvenByAuthor(userId),
                    Id = userId
                    
                },
                Id = userId,
                IsOwner = (User.Identity.GetUserId() == userId)

            };
          
            return View(vm);
        }

        [HttpGet]
        public ActionResult Schedule(string userId, DateTime date)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(string userId)
        {
            var user = new SheduleService().GetUserById(userId);
            var vm = new ProfileViewModel()
            {
                UserInfo = new UserInfoViewModel
                {
                    IsProfileAdmin = new SheduleService().IsAdmin(user),
                    Name = user.UserName,
                    Email = user.Email,
                    ActiveEventsCount = new SheduleService().GetEvenByAuthor(userId),
                    Id = userId

                },
                Id = userId,
                IsOwner = (User.Identity.GetUserId() == userId)

            };
            return PartialView("_EdetProfilePartial", vm);
        }        
    }
}