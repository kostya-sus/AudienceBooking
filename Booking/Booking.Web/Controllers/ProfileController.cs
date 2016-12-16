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
            var user = new UsersService().GetUserById(userId);
            var vm = new ProfileViewModel()
            {
                UserInfo = new UserInfoViewModel
                {
                    IsProfileAdmin = new UsersService().IsAdmin(user),
                    Name = user.UserName,
                    Email = user.Email,
                    ActiveEventsCount = new UsersService().GetEvenByAuthor(userId),
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
        public ActionResult Edit(UserInfoViewModel userInfoModeld)
        {
            return PartialView("_EditProfilePartial", userInfoModeld);
        }
        
       
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(string userId)
        {
            throw new NotImplementedException();
            //  ApplicationUser user = await UserManager.FindByIdAsync(userId);
            //  if (user != null)
            //  {
            //      IdentityResult result = await UserManager.DeleteAsync(user);
            //      if (result.Succeeded)
            //      {
            //          return RedirectToAction("Index", "Home");
            //      }
            //  }
            //  return RedirectToAction("Index", "Home");

            //  var db = new BookingDbContext();
            //  var user = db.Users.Find(userId);
            //  db.Users.Remove(user);
            //  db.SaveChanges();
            //  return RedirectToAction("Index","Home");
        }

        
    }
}