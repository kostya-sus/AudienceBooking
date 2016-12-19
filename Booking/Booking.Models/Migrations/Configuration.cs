using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Text;
using Booking.Enums;
using Booking.Models.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Booking.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Booking.Models.BookingDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Booking.Models.BookingDbContext context)
        {
            var roleStore = new RoleStore<IdentityRole>(context);
            const string role = "Admin";

            if (!context.Roles.Any(r => r.Name == role))
            {
                roleStore.CreateAsync(new IdentityRole(role)).Wait();
            }
            
            var admin = CreateUser(context, "Admin", "admin@user.fake");

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            userManager.AddToRole(admin.Id, "Admin");
        }
        
        private ApplicationUser CreateUser(BookingDbContext context, string username, string email)
        {
            if (context.Users.Any(u => u.UserName == username)) return context.Users.First(u => u.UserName == username);
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var userToInsert = new ApplicationUser {UserName = username, Email = email};
            userManager.Create(userToInsert, "password");
            return userToInsert;
        }
    }
}