using Booking.Web.ViewModels.Audience;
using Booking.Web.ViewModels.Manage;

namespace Booking.Web.ViewModels.Profile
{
    public class ProfileViewModel
    {
        public UserInfoViewModel UserInfo { get; set; }

        public ChangePasswordViewModel ChangePasswordForm { get; set; }

        public ScheduleTableViewModel ScheduleTable { get; set; }

        public bool IsOwner { get; set; }

        public bool IsAdmin { get; set; }
    }
}