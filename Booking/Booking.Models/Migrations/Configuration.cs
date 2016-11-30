using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using Booking.Enums;
using Booking.Models.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Booking.Models.Migrations
{
    using System;
    using System.Data.Entity;
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
            SeedAudiences(context);

            var roleStore = new RoleStore<IdentityRole>(context);
            const string role = "Admin";

            if (!context.Roles.Any(r => r.Name == role))
            {
                roleStore.CreateAsync(new IdentityRole(role)).Wait();
            }
            
            var eventTitles = new string[]
            {
                "English class", "English class - Speaking for Juniors", "English class - Speaking for Seniors",
                "Android team meeting"
            };

            var dates = GetDates();

            var admin = CreateUser(context, "Admin");

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            userManager.AddToRole(admin.Id, "Admin");

            var doe = CreateUser(context, "John Doe");
            var smith = CreateUser(context, "John Smith");

            var random = new Random();
            foreach (var date in dates)
            {
                CreateEvent(context, doe, date, random.Choice(eventTitles), random.Next(8) != 0,
                    AudiencesEnum.EinsteinClassroom);

                CreateEvent(context, smith, date, random.Choice(eventTitles), random.Next(8) != 0,
                    AudiencesEnum.NewtonClassroom);
            }
        }

        private List<DateTime> GetDates()
        {
            var random = new Random();

            var dates = new List<DateTime>();

            var today = DateTime.Today;
            int hours = (int) BookingHoursBoundsEnum.Lower;
            int minutes = 0;

            for (int i = 0; i < 30; i++)
            {
                for (int j = 0; j < 5; ++j)
                {
                    int minimumHour = hours + 1 + minutes/60;
                    hours = random.Next(minimumHour, minimumHour + 5);
                    minutes = 30*random.Next(2);

                    if (hours > (int) BookingHoursBoundsEnum.Upper)
                    {
                        hours = (int) BookingHoursBoundsEnum.Upper;
                        j = 6;
                        continue;
                    }

                    var date = today.AddDays(i).AddHours(hours).AddMinutes(minutes);
                    dates.Add(date);
                }
            }

            return dates;
        }

        private void CreateEvent(BookingDbContext context, ApplicationUser author, DateTime date, string title,
            bool isPublic, AudiencesEnum audienceId)
        {
            var random = new Random();

            var eventEntity = new Event
            {
                AudienceId = audienceId,
                Title = title,
                IsPublic = isPublic,
                IsJoinAvailable = isPublic,
                EventDateTime = date,
                Duration = random.Next(2)*30,
                AuthorId = author.Id
            };

            context.Events.Add(eventEntity);
            context.SaveChanges();
        }

        private void SeedAudiences(BookingDbContext context)
        {
            var audiences = new List<Audience>
            {
                new Audience
                {
                    Id = AudiencesEnum.HrOffice,
                    Name = "HR office",
                    SeatsCount = 10,
                    LaptopsCount = 5,
                    PrintersCount = 1,
                    IsBookingAvailable = false
                },
                new Audience
                {
                    Id = AudiencesEnum.EinsteinClassroom,
                    Name = "Einstein Classroom",
                    SeatsCount = 15,
                    BoardsCount = 1,
                    ProjectorsCount = 1,
                    LaptopsCount = 10,
                    IsBookingAvailable = true
                },
                new Audience
                {
                    Id = AudiencesEnum.InfoCenter,
                    Name = "Info center",
                    SeatsCount = 10,
                    LaptopsCount = 5,
                    PrintersCount = 1,
                    IsBookingAvailable = false
                },
                new Audience
                {
                    Id = AudiencesEnum.WebAndMarketing1,
                    Name = "Web & Marketing",
                    SeatsCount = 10,
                    LaptopsCount = 5,
                    PrintersCount = 1,
                    IsBookingAvailable = false
                },
                new Audience
                {
                    Id = AudiencesEnum.WebAndMarketing2,
                    Name = "Web & Marketing",
                    SeatsCount = 10,
                    LaptopsCount = 5,
                    PrintersCount = 1,
                    IsBookingAvailable = false
                },
                new Audience
                {
                    Id = AudiencesEnum.WebAndMarketing3,
                    Name = "Web & Marketing",
                    SeatsCount = 10,
                    LaptopsCount = 5,
                    PrintersCount = 1,
                    IsBookingAvailable = false
                },
                new Audience
                {
                    Id = AudiencesEnum.English,
                    Name = "English",
                    SeatsCount = 5,
                    LaptopsCount = 3,
                    PrintersCount = 1,
                    IsBookingAvailable = false
                },
                new Audience
                {
                    Id = AudiencesEnum.TeslaClassroom,
                    Name = "Tesla Classroom",
                    SeatsCount = 15,
                    BoardsCount = 1,
                    ProjectorsCount = 1,
                    LaptopsCount = 10,
                    IsBookingAvailable = true
                },
                new Audience
                {
                    Id = AudiencesEnum.NewtonClassroom,
                    Name = "Newton Classroom",
                    SeatsCount = 15,
                    BoardsCount = 1,
                    ProjectorsCount = 1,
                    LaptopsCount = 10,
                    IsBookingAvailable = true
                },
            };

            foreach (var audience in audiences)
            {
                if (!context.Audiences.Any(a => a.Id == audience.Id))
                {
                    context.Audiences.Add(audience);
                }
            }

            context.SaveChanges();
        }

        private ApplicationUser CreateUser(BookingDbContext context, string username)
        {
            if (context.Users.Any(u => u.UserName == username)) return context.Users.First(u => u.UserName == username);
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var userToInsert = new ApplicationUser {UserName = username};
            userManager.Create(userToInsert, "password");
            return userToInsert;
        }
    }
}