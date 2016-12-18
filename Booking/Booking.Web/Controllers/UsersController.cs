using Booking.Services.Services;
using System;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using Microsoft.AspNet.Identity;
using System.Web;
using Microsoft.AspNet.Identity.Owin;

namespace Booking.Web.Controllers
{
    public class UsersController : Controller
    {
        
        public ActionResult Index()
        {
            return View();

        }
    }
}